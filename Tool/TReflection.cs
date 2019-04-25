using System;
using System.Collections.Generic;
using System.Reflection;

namespace Tool
{
    public class TReflection
    {
        private static readonly Lazy<TReflection> lazyInstance = new Lazy<TReflection>(() => new TReflection());
        public static TReflection Instance
        {
            get
            {
                return lazyInstance.Value;
            }
        }

        /// <summary>
        /// 实例化某命名空间下所有类
        /// </summary>
        /// <typeparam name="T">类的类型</typeparam>
        /// <param name="nameSpace">命名空间</param>
        /// <returns></returns>
        public List<T> GetClasses<T>(string nameSpace)
        {
            Assembly asm = Assembly.GetCallingAssembly();

            List<string> classNameList = new List<string>();
            List<T> classList = new List<T>();

            foreach (Type type in asm.GetTypes())
            {
                if (type.Namespace == nameSpace)
                    classNameList.Add(type.Name);
            }

            foreach(var name in classNameList)
            {
                string fullName = $"{nameSpace}.{name}";
                object ect = asm.CreateInstance(fullName);
                classList.Add((T)ect);
            }

            return classList;
        }
    }
}
