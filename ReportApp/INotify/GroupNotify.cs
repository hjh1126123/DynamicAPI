using EntityLocal;
using PropertyChanged;
using System.Collections.Generic;

namespace ReportApp.INotify
{
    [ImplementPropertyChanged]
    public class GroupNotify
    {
        public string Name { get; set; }
        public string Describe { get; set; }
        public List<BGroup> Groups { get; set; }
        
    }
}
