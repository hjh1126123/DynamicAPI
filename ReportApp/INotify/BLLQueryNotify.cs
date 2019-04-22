using ReportApp.Extension;
using ReportApp.Model;
using System;
using System.ComponentModel;

namespace ReportApp.INotify
{
    public class BLLQueryNotify : INotifyPropertyChanged
    {
        private MQuery bllQuery;
        public MQuery BllQuery
        {
            get { return bllQuery; }
            set { this.MutateVerbose(ref bllQuery, value, RaisePropertyChanged()); }
        }

        public BLLQueryNotify() { }

        public BLLQueryNotify(MQuery mbllQuery)
        {
            bllQuery = mbllQuery;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
    }
}
