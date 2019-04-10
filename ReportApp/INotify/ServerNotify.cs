using ServerApp.Extension;
using ServerApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ServerApp.INotify
{
    public class ServerNotify : INotifyPropertyChanged
    {
        IEnumerable<ServerControlModel> serverControlModels;

        public ServerNotify() { }

        public ServerNotify(IEnumerable<ServerControlModel> serverControlModel)
        {
            serverControlModels = serverControlModel;
        }

        public IEnumerable<ServerControlModel> ServerControls
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
