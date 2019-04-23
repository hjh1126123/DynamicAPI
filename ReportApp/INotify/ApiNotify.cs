using EntityLocal;
using PropertyChanged;
using System.Collections.Generic;

namespace ReportApp.INotify
{
    [ImplementPropertyChanged]
    public class ApiNotify
    {
        public List<IApi> Apis { get; set; }
        public List<string> Pattern { get; set; }
        public string Name { get; set; }
        public string RequestKey { get; set; }
        public string Describe { get; set; }
        public List<string> Charts { get; set; }
    }
}
