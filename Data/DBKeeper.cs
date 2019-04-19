using System;
using System.Collections.Concurrent;

namespace Data
{
    public class DBKeeper
    {
        /// <summary>
        /// 懒加载单例
        /// </summary>
        private static readonly Lazy<DBKeeper> lazyInstance = new Lazy<DBKeeper>(() => new DBKeeper());

        /// <summary>
        /// 实例
        /// </summary>
        public static DBKeeper Instance
        {
            get
            {
                return lazyInstance.Value;
            }
        }

        /// <summary>
        /// 数据组件存储对象
        /// </summary>
        private readonly ConcurrentDictionary<string, DBComponent> componentsSave;

        /// <summary>
        /// 获取数据组件
        /// </summary>
        /// <typeparam name="T">数据组件</typeparam>
        /// <returns></returns>
        public T DBObject<T>() where T : DBComponent, new()
        {
            string ObjName = typeof(T).Name;
            if (componentsSave.ContainsKey(ObjName))
            {
                componentsSave.TryGetValue(ObjName, out DBComponent dBComponent);
                return (T)dBComponent;
            }
            else
            {
                T t = new T();
                componentsSave.AddOrUpdate(ObjName, t, (k, v) => t);
                return t;
            }
        }

        private DBKeeper()
        {
            componentsSave = new ConcurrentDictionary<string, DBComponent>();
        }
    }
}
