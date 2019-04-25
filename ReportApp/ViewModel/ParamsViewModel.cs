using PropertyChanged;
using Server.DBLocal;
using System.Collections.Generic;

namespace ReportApp.ViewModel
{
    [ImplementPropertyChanged]
    public class ParamsViewModel
    {
        public string Name { get; set; }
        public string Describe { get; set; }
        public string Key { get; set; }
        public bool Multiple { get; set; }
        public List<BParam> Params { get; set; }
    }
}
