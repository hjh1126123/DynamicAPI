using Tool;
using System.Collections.Concurrent;

namespace Server.Strategy
{
    public class StrategyKeeper : IServer
    {
        public ConcurrentDictionary<string, StrategyModel> Strategys;
        public StrategyKeeper(ServerKeeper serverKeeper) : base(serverKeeper)
        {
            Strategys = new ConcurrentDictionary<string, StrategyModel>();

            StrategyModel strategyNone = new StrategyModel { Name = "不选择", Describe = "无任何集成策略" };
            Strategys.AddOrUpdate(strategyNone.Name, strategyNone, (_oK, _oV) => strategyNone);            

            foreach (var item in TReflection.Instance.GetClasses<IStrategy>("Server.Strategy.StrategyComponents"))
            {
                StrategyModel strategyModel = new StrategyModel
                {
                    Name = item.Name(),
                    Describe = item.Describe(),
                    Strategy = item
                };

                Strategys.AddOrUpdate(strategyModel.Name, strategyModel, (_oK, _oV) => strategyModel);
            }
        }
    }
}
