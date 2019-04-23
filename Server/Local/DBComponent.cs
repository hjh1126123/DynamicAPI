using Devart.Common;
using EntityLocal;
using System;
using Server.Local;

namespace Server
{
    public abstract class DBComponent
    {
        /// <summary>
        /// 数据执行函数
        /// </summary>
        /// <typeparam name="T">数据块</typeparam>
        /// <param name="func">执行委托</param>
        /// <returns></returns>
        protected T Context<T>(Func<DBLocal, T> func)
        {
            T tmp = default;
            DBLocal dBContext = null;
            try
            {
                dBContext = new DBLocal();
                tmp = func(dBContext);
            }
            catch(Devart.Data.SQLite.SQLiteException ex)
            {
                DBKeeper.Instance.DBObject<S_Log>().Error("SQLiteException", $"错误信息：{ex.InnerException.Message}\r位置：{ex.InnerException.StackTrace}");
            }
            catch(Devart.Data.Linq.LinqCommandExecutionException ex)
            {
                DBKeeper.Instance.DBObject<S_Log>().Error("LinqCommandExecutionException", $"错误信息：{ex.InnerException.Message}\r位置：{ex.InnerException.StackTrace}");
            }
            catch(Exception ex)
            {
                DBKeeper.Instance.DBObject<S_Log>().Error("Exception", $"错误信息：{ex.InnerException.Message}\r位置：{ex.InnerException.StackTrace}");
            }
            finally
            {
                if (dBContext == null)
                    dBContext.Dispose();
            }
            return tmp;
        }
    }
}
