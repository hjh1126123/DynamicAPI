using System.Collections.Concurrent;

namespace Server.DBLocal
{
    public class DBLocalKeeper : IServer
    {
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
                T components = new T();
                componentsSave.AddOrUpdate(ObjName, components, (k, v) => components);
                return components;
            }
        }

        public DBLocalKeeper(ServerKeeper serverKeeper) : base(serverKeeper)
        {
            componentsSave = new ConcurrentDictionary<string, DBComponent>();
        }
    }
}
