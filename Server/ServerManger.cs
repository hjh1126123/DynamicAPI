using Server.Strategy;
using System;
using System.Collections.Generic;

namespace Server
{
    public class ServerManger
    {
        private static readonly Lazy<ServerManger> lazyInstance = new Lazy<ServerManger>(() => new ServerManger());
        public static ServerManger Instance
        {
            get
            {
                return lazyInstance.Value;
            }
        }

        public List<StrategyModel> Strategys;
        private ServerManger()
        {
            Strategys = new List<StrategyModel>
            {
                new StrategyModel{ Name = "不选择", Describe = "无任何集成策略"},
                new StrategyModel{ Name = "ExPackageStrategy", Describe = "包裹表的整理和汇总", Strategy = new ExPackageStrategy() }
            };
        }
    }
}
