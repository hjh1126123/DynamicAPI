using Server.Local;
using System;
using System.Collections.Concurrent;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Tool;

namespace Server.Net
{
    /// <summary>
    /// 参数化模型
    /// </summary>
    public class SqlParameterModel
    {
        string pid;
        object value;
        List<string> values;

        public string Pid { get => pid; set => pid = value; }
        public object Value { get => value; set => this.value = value; }
        public List<string> Values { get => values; set => values = value; }
    }

    /// <summary>
    /// 数据控制器
    /// </summary>
    public class ConnNet
    {
        const string connectionStringKey = "Net";


        private struct SQL
        {
            public string key;
            public SqlConnection sqlConnection;
        }
        private static readonly Lazy<ConnNet> lazyInstance = new Lazy<ConnNet>(() => new ConnNet());
        public static ConnNet Instance
        {
            get
            {
                return lazyInstance.Value;
            }
        }

        private ConcurrentQueue<SQL> isUnUse;
        private ConcurrentDictionary<string, SqlConnection> isUse;
        private readonly string connText = string.Empty;
        private readonly int maxConn = 0;

        /// <summary>
        /// 将对象放入[正在使用]池
        /// </summary>
        private void ChangeUsing(SQL sQL)
        {
            isUse.AddOrUpdate(sQL.key, sQL.sqlConnection, (oldkey, oldvalue) => sQL.sqlConnection);
        }

        /// <summary>
        /// 等待锁
        /// </summary>
        private readonly object waitLock = new object();
        /// <summary>
        /// 等待一个对象
        /// </summary>
        /// <returns></returns>
        private SQL WaitOne()
        {
            lock (waitLock)
            {
                return Task.Run(() =>
                {
                    SQL sQL;
                    while (true)
                    {
                        Thread.Sleep(300);
                        if (isUnUse.Count > 0)
                        {
                            if (isUnUse.TryDequeue(out sQL))
                            {
                                break;
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    return sQL;
                }).Result;
            }
        }

        /// <summary>
        /// 打开连接
        /// </summary>
        /// <returns></returns>
        private SQL OpenConn()
        {
            SQL sQL;
            if (isUnUse.Count <= 0)
            {
                if (isUse.Count >= maxConn)
                {
                    sQL = WaitOne();
                }
                else
                {
                    while (true)
                    {
                        sQL.key = TRandom.Instance.GetRandomString(12);
                        if (isUse.ContainsKey(sQL.key))
                        {
                            continue;
                        }
                        break;
                    }
                    sQL.sqlConnection = new SqlConnection(connText);
                }
                ChangeUsing(sQL);
            }
            else
            {
                if (isUnUse.TryDequeue(out sQL))
                {
                    ChangeUsing(sQL);
                }
                else
                {
                    DBKeeper.Instance.DBObject<S_Log>().Error("DataControl-OpenConn-CustomEx", $"说明：isUnUse队列移除失败 \r位置：DataControl 114");
                }
            }

            if (sQL.sqlConnection.State == ConnectionState.Open)
            {
                DBKeeper.Instance.DBObject<S_Log>().Error("DataControl-OpenConn-CustomEx", $"说明：数据库连接仍然处在开启状态 \r位置：DataControl 126");
            }
            else
            {
                sQL.sqlConnection.Open();
            }

            return sQL;
        }

        /// <summary>
        /// 回收连接
        /// </summary>
        /// <param name="sQL"></param>
        private void FreeConn(SQL sQL)
        {
            try
            {
                if (sQL.sqlConnection.State != ConnectionState.Closed)
                    sQL.sqlConnection.Close();

                isUnUse.Enqueue(sQL);

                isUse.TryRemove(sQL.key, out SqlConnection test);
            }
            catch (ArgumentNullException ex)
            {
                DBKeeper.Instance.DBObject<S_Log>().Error("DataControl-FreeConn-ArgumentNullException", $"说明：{ex.InnerException.Message}\r位置：{ex.InnerException.StackTrace}");
            }
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="model"></param>
        private void AddParameters(SqlCommand sqlCommand, List<SqlParameterModel> pids)
        {
            foreach (var pid in pids)
            {
                var bPar = DBKeeper.Instance.DBObject<B_Params>().Select(pid.Pid);
                if (bPar.Multiple.GetValueOrDefault())
                {
                    for (int count = 0; count < pid.Values.Count; count++)
                    {
                        sqlCommand.Parameters.Add(new SqlParameter($"{bPar.Key}{count}", pid.Values[count]));
                    }
                }
                else
                {
                    sqlCommand.Parameters.Add(new SqlParameter(bPar.Key, pid.Value));
                }
            }
        }

        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="data">数据内容</param>
        /// <param name="params">参数</param>
        /// <returns></returns>
        public DataTable Select(string SQL, List<SqlParameterModel> pids)
        {
            SQL sQL = OpenConn();
            SqlCommand sqlCommand = null;
            SqlDataAdapter sqlDataAdapter = null;
            DataSet dataSet = null;
            DataTable dataTable = null;
            try
            {
                sqlCommand = sQL.sqlConnection.CreateCommand();
                sqlCommand.CommandText = SQL;
                AddParameters(sqlCommand, pids);
                sqlCommand.ExecuteNonQuery();
                sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);
                dataTable = dataSet.Tables[0];
            }
            catch (Exception ex)
            {
                DBKeeper.Instance.DBObject<S_Log>().Error("DataControl-Select-Exception", $"说明：{ex.InnerException.Message}\r位置：{ex.InnerException.StackTrace}");
            }
            finally
            {
                if (dataSet != null)
                    dataSet.Dispose();

                if (sqlDataAdapter != null)
                    sqlDataAdapter.Dispose();

                if (sqlCommand != null)
                    sqlCommand.Dispose();

                FreeConn(sQL);
            }
            return dataTable;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="maxConn">最大连接数</param>
        public ConnNet(int maxConn = 20)
        {
            connText = ConfigurationManager.ConnectionStrings[connectionStringKey].ConnectionString;
            isUnUse = new ConcurrentQueue<SQL>();
            isUse = new ConcurrentDictionary<string, SqlConnection>();
            this.maxConn = maxConn;
        }
    }
}
