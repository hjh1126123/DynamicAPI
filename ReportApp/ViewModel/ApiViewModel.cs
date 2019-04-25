using PropertyChanged;
using Server.DBLocal;
using System.Collections.Generic;

namespace ReportApp.ViewModel
{
    [ImplementPropertyChanged]
    public class ApiViewModel
    {
        public List<IApi> Apis { get; set; }
        public List<string> Pattern { get; set; }
        public string Name { get; set; }
        public string RequestKey { get; set; }
        public string Describe { get; set; }
        public List<string> Charts { get; set; }
    }
}
