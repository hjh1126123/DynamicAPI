using EntityLocal;
using PropertyChanged;
using System.Collections.Generic;
using System.ComponentModel;

namespace ReportApp.INotify
{
    public class CheckBoxParams
    {
        public bool Checked { get; set; }
        public BParam BParam { get; set; }
    }

    [ImplementPropertyChanged]
    public class AddNotify
    {
        public List<BGroup> Groups { get; set; }
        public BindingList<CheckBoxParams> Params { get; set; }
        public List<IApi> Apis { get; set; } 
        public string ActiveName { get; set; }
        public string ActiveDescribe { get; set; }
        public string Sql { get; set; }
        public string Name { get; set; }
        public string TimeField { get; set; }
        public string ValueField { get; set; }
        public string PreFix { get; set; }
        public string SufFix { get; set; }
        public string OtherData { get; set; }
    }
}
