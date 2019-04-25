using System.Data;

namespace Server.Strategy
{
    public interface StrategyAction
    {
        DataTable Operator(DataTable dataTable);
    }

    public abstract class IStrategy : StrategyAction
    {        
        public abstract string Name();
        public abstract string Describe();

        protected abstract DataTable Run(DataTable dataTable);
        public DataTable Operator(DataTable dataTable)
        {
            return Run(dataTable);
        }
    }
}
