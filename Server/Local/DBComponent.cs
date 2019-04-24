using Server.Local;
using System;

namespace Server
{
    public abstract class DBComponent
    {
        /// <summary>
        /// 数据执行函数（异常写入日志）
        /// </summary>
        /// <typeparam name="T">数据块</typeparam>
        /// <param name="func">执行委托</param>
        /// <returns></returns>
        protected T Context<T>(Func<DBLocal, T> func)
        {
            T tmp = default;
            DBLocal dBLocal = null;
            try
            {
                dBLocal = new DBLocal();
                tmp = func(dBLocal);
            }
            catch (Devart.Data.SQLite.SQLiteException ex)
            {
                DBKeeper.Instance.DBObject<S_Log>().Error("SQLiteException", $"错误信息：{ex.InnerException.Message}\r位置：{ex.InnerException.StackTrace}");
            }
            catch (Devart.Data.Linq.LinqCommandExecutionException ex)
            {
                DBKeeper.Instance.DBObject<S_Log>().Error("LinqCommandExecutionException", $"错误信息：{ex.InnerException.Message}\r位置：{ex.InnerException.StackTrace}");
            }
            catch (Exception ex)
            {
                DBKeeper.Instance.DBObject<S_Log>().Error("Exception", $"错误信息：{ex.InnerException.Message}\r位置：{ex.InnerException.StackTrace}");
            }
            finally
            {
                if (dBLocal == null)
                    dBLocal.Dispose();
            }
            return tmp;
        }

        /// <summary>
        /// 数据执行函数（异常不写入日志）
        /// </summary>
        /// <typeparam name="T">数据块</typeparam>
        /// <param name="func">执行委托</param>
        /// <returns></returns>
        protected T ContextNoLog<T>(Func<DBLocal, T> func)
        {
            T tmp = default;
            DBLocal dBLocal = null;
            try
            {
                dBLocal = new DBLocal();
                tmp = func(dBLocal);
            }
            catch (Devart.Data.SQLite.SQLiteException ex)
            {
                throw ex;
            }
            catch (Devart.Data.Linq.LinqCommandExecutionException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dBLocal == null)
                    dBLocal.Dispose();
            }
            return tmp;
        }
    }
}
