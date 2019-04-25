using System.Data;

namespace Server.Strategy.StrategyComponents
{
    public class ExPackageStrategy : IStrategy
    {
        public override string Describe()
        {
            return "将异常包裹数据进行整理排列";
        }

        public override string Name()
        {
            return "ExPackage";
        }

        protected override DataTable Run(DataTable dataTable)
        {
            return dataTable;
        }
    }
}
