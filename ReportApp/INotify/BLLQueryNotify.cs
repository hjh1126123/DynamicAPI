using ServerApp.Extension;
using ServerApp.Model;
using System;
using System.ComponentModel;

namespace ServerApp.INotify
{
    public class BLLQueryNotify : INotifyPropertyChanged
    {
        private MBLLQuery bllQuery;
        public MBLLQuery SQLMaintenanceModel
        {
            get { return bllQuery; }
            set { this.MutateVerbose(ref bllQuery, value, RaisePropertyChanged()); }
        }

        public BLLQueryNotify() { }

        public BLLQueryNotify(MBLLQuery mbllQuery)
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
