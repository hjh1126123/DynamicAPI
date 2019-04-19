using EntityLocal;
using System;
using System.Collections.Generic;

namespace Data
{
    public abstract class DBComponent
    {
        /// <summary>
        /// 数据执行函数
        /// </summary>
        /// <typeparam name="T">数据块</typeparam>
        /// <param name="func">执行委托</param>
        /// <returns></returns>
        protected T Context<T>(Func<DBContext, T> func)
        {
            DBContext dBContext = null;
            try
            {
                dBContext = new DBContext();
                return func(dBContext);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dBContext == null)
                    dBContext.Dispose();
            }
        }
    }
}
