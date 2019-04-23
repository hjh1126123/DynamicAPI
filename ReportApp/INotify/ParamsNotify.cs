using EntityLocal;
using PropertyChanged;
using System.Collections.Generic;

namespace ReportApp.INotify
{
    [ImplementPropertyChanged]
    public class ParamsNotify
    {
        public string Name { get; set; }
        public string Describe { get; set; }
        public string Key { get; set; }
        public List<BParam> Params { get; set; }
    }
}
