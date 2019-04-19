using ServerApp.Extension;
using ServerApp.Model;
using System;
using System.ComponentModel;

namespace ServerApp.INotify
{
    public class BLLAddNotify : INotifyPropertyChanged
    {
        private MBLLAdd bllAdd;
        public MBLLAdd BllAdd
        {
            get { return bllAdd; }
            set { this.MutateVerbose(ref bllAdd, value, RaisePropertyChanged()); }
        }

        public BLLAddNotify() { }

        public BLLAddNotify(MBLLAdd mBllAdd)
        {
            bllAdd = mBllAdd;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
    }
}
