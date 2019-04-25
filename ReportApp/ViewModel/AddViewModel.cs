using PropertyChanged;
using Server.DBLocal;
using System.Collections.Generic;
using System.ComponentModel;
using Server.Strategy;

namespace ReportApp.ViewModel
{
    public class CheckBoxParams
    {
        public bool Checked { get; set; }
        public BParam BParam { get; set; }
    }

    [ImplementPropertyChanged]
    public class AddViewModel
    {
        public List<BGroup> Groups { get; set; }
        public BindingList<CheckBoxParams> Params { get; set; }
        public List<IApi> Apis { get; set; }
        public List<StrategyModel> Strategys { get; set; }
        public string ActiveName { get; set; }
        public string ActiveDescribe { get; set; }
        public string Sql { get; set; }
        public string Name { get; set; }        
    }
}
