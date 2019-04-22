using ReportApp.Extension;
using ReportApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ReportApp.INotify
{
    public class ServerNotify : INotifyPropertyChanged
    {
        IEnumerable<MServerControl> serverControlModels;

        public ServerNotify() { }

        public ServerNotify(IEnumerable<MServerControl> serverControlModel)
        {
            serverControlModels = serverControlModel;
        }

        public IEnumerable<MServerControl> ServerControls
        {
            get { return serverControlModels; }
            set { this.MutateVerbose(ref serverControlModels, value, RaisePropertyChanged()); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
    }
}
