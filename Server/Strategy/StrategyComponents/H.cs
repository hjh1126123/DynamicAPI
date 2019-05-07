using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Strategy.StrategyComponents
{
    public class H : IStrategy
    {
        public override string Describe()
        {
            return "很黄";
        }

        public override string Name()
        {
            return "H";
        }

        protected override DataTable Run(DataTable dataTable)
        {
            return null;
        }
    }
}
