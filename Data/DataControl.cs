using System;
using System.Collections.Concurrent;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Util;

namespace Data
{
    /// <summary>
    /// 数据控制器
    /// </summary>
    public class DataControl
    {
        private struct SQL
        {
            public string key;
            public SqlConnection sqlConnection;
        }

        const string connectionStringKey = "Conn";
        private static readonly Lazy<DataControl> lazyInstance = new Lazy<DataControl>(() => new DataControl());
        public static DataControl Instance
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
        public DataControl(int maxConn = 10)
        {
            connText = ConfigurationManager.ConnectionStrings[connectionStringKey].ConnectionString;
            isUnUse = new ConcurrentQueue<SQL>();
            isUse = new ConcurrentDictionary<string, SqlConnection>();
            this.maxConn = maxConn;
        }

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
                        sQL.key = URandom.Instance.GetRandomString(12);
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
                    //...输出错误日志
                }
            }

            if (sQL.sqlConnection.State == ConnectionState.Open)
            {
                //...输出错误日志
            }

            sQL.sqlConnection.Open();

            return sQL;
        }

        /// <summary>
        /// 回收连接
        /// </summary>
        /// <param name="sQL"></param>
        private void FreeConn(SQL sQL)
        {
            if (sQL.sqlConnection.State != ConnectionState.Closed)
                sQL.sqlConnection.Close();

            isUnUse.Enqueue(sQL);

            if (isUse.TryRemove(sQL.key, out SqlConnection test))
            {
                //...输出错误日志
            }
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="model"></param>
        private void AddParameters(SqlCommand sqlCommand, DataModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.Value))
            {
                if (model.Condition.Equals("in"))
                {
                    string[] tmpValues = model.Value.Split(',');
                    for (int vCount = 0; vCount < tmpValues.Length; vCount++)
                    {
                        sqlCommand.Parameters.Add(new SqlParameter(model.ParKey + vCount, tmpValues[vCount]));
                    }
                }
                else
                {
                    sqlCommand.Parameters.Add(new SqlParameter(model.ParKey, model.Value));
                }
            }
        }

        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="data">数据内容</param>
        /// <param name="params">参数</param>
        /// <returns></returns>
        public DataTable Select(IData data)
        {
            SQL sQL = OpenConn();
            SqlCommand sqlCommand = null;
            SqlDataAdapter sqlDataAdapter = null;
            DataSet dataSet = null;
            DataTable dataTable = null;
            try
            {
                sqlCommand = sQL.sqlConnection.CreateCommand();
                sqlCommand.CommandText = data.SQL;
                foreach (var key in data.dictParams.Keys)
                {
                    foreach (var model in data.dictParams[key])
                    {
                        AddParameters(sqlCommand, model);
                    }
                }
                sqlCommand.ExecuteNonQuery();

                sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                dataSet = new DataSet();

                sqlDataAdapter.Fill(dataSet);

                dataTable = dataSet.Tables[0];
            }
            catch (Exception ex)
            {
                //...输出错误日志
                throw ex;
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
    }
}
