using ServerApp.Extension;
using ServerApp.Model;
using System;
using System.ComponentModel;

namespace ServerApp.INotify
{
    public class SQLMaintenanceNotify : INotifyPropertyChanged
    {
        private MSQLMaintenance sQLMaintenanceModel;
        public MSQLMaintenance SQLMaintenanceModel
        {
            get { return sQLMaintenanceModel; }
            set { this.MutateVerbose(ref sQLMaintenanceModel, value, RaisePropertyChanged()); }
        }

        public SQLMaintenanceNotify() { }

        public SQLMaintenanceNotify(MSQLMaintenance mSQLMaintenance)
        {
            sQLMaintenanceModel = mSQLMaintenance;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
    }
}
