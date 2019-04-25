using Server.DBLocal;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Tool;

namespace Server.DBNet
{
    /// <summary>
    /// 数据控制器
    /// </summary>
    public class DBNetKeeper : IServer
    {        
        private struct SQL
        {
            public string key;
            public SqlConnection sqlConnection;
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

        /// <summary
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
                    serverKeeper.DBLocalKeeper.DBObject<S_Log>().Error("DataControl-OpenConn-CustomEx", $"说明：isUnUse队列移除失败 \r位置：DataControl 114");
                }
            }

            if (sQL.sqlConnection.State == ConnectionState.Open)
            {
                serverKeeper.DBLocalKeeper.DBObject<S_Log>().Error("DataControl-OpenConn-CustomEx", $"说明：数据库连接仍然处在开启状态 \r位置：DataControl 126");
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
                serverKeeper.DBLocalKeeper.DBObject<S_Log>().Error("DataControl-FreeConn-ArgumentNullException", $"说明：{ex.InnerException.Message}\r位置：{ex.InnerException.StackTrace}");
            }
        }


        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="model"></param>
        private void AddParameters(SqlCommand sqlCommand, List<string> pids)
        {
            foreach (var pid in pids)
            {
                BParam bPar = serverKeeper.DBLocalKeeper.DBObject<B_Params>().Select(pid);
                IParams iPar = null;
                if (ParamsSave.ContainsKey(bPar.Key))
                {            
                    iPar = ParamsSave[bPar.Key];
                }
                else
                    continue;


                if (bPar.Multiple.GetValueOrDefault())
                {
                    string parKeys = "(";
                    for (int count = 0; count < iPar.GetValues().Count; count++)
                    {
                        string tmpKey = $"{bPar.Key}{count}";
                        sqlCommand.Parameters.Add(new SqlParameter(tmpKey, iPar.GetValues()[count]));
                        parKeys += tmpKey;
                        if (count < iPar.GetValues().Count - 1)
                            parKeys += ',';
                    }
                    parKeys += ")";
                    sqlCommand.CommandText = sqlCommand.CommandText.Replace(bPar.Key, parKeys);
                }
                else
                {
                    sqlCommand.Parameters.Add(new SqlParameter(bPar.Key, iPar.GetValue()));
                }
            }
        }

        /// <summary>
        /// 执行查询带参数
        /// </summary>
        /// <param name="data">数据内容</param>
        /// <param name="params">参数</param>
        /// <returns></returns>
        public DataTable Select(string SQL, List<string> pids)
        {
            return Select(SQL, sqlcommand =>
             {
                 AddParameters(sqlcommand, pids);
             });            
        }

        /// <summary>
        /// 执行查询不带参数
        /// </summary>
        /// <param name="SQL"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public DataTable Select(string SQL, Action<SqlCommand> action = null)
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
                action?.Invoke(sqlCommand);
                sqlCommand.ExecuteNonQuery();
                sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);
                dataTable = dataSet.Tables[0];
            }
            catch (SqlException ex)
            {
                serverKeeper.DBLocalKeeper.DBObject<S_Log>().Error("DataControl-Select-Exception", $"说明：{ex.Message}\r位置：{ex.StackTrace}");
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
        /// 参数保存
        /// </summary>
        public ConcurrentDictionary<string, IParams> ParamsSave;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serverKeeper">服务管理员</param>
        /// <param name="connectionStringKey">连接字段(app.config)</param>
        /// <param name="maxConn"></param>
        public DBNetKeeper(ServerKeeper serverKeeper, string connectionStringKey, int maxConn = 20) : base(serverKeeper)
        {
            connText = ConfigurationManager.ConnectionStrings[connectionStringKey].ConnectionString;
            isUnUse = new ConcurrentQueue<SQL>();
            isUse = new ConcurrentDictionary<string, SqlConnection>();

            //最大连接数绑定
            this.maxConn = maxConn;

            ParamsSave = new ConcurrentDictionary<string, IParams>();
            //参数导入
            foreach (var item in TReflection.Instance.GetClasses<IParams>("Server.Net.ParamsComponents"))
            {                
                ParamsSave.AddOrUpdate(item.GetKey(), item, (o_k, o_v) => item);
            }
        }
    }
}
