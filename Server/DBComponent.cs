using EntityLocal;
using System;
using System.Collections.Generic;

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
        protected T Context<T>(Func<DBContext, T> func)
        {
            T tmp = default;
            DBContext dBContext = null;
            try
            {
                dBContext = new DBContext();
                tmp = func(dBContext);
            }
            catch (Exception ex)
            {
                //将 ex 写入日志
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
