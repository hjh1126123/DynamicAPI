using System;
using System.Collections.Generic;
using System.ComponentModel;

using ServerApp.Model;
using ServerApp.Extension;

namespace ServerApp.INotify
{
    public class HomeNotify : INotifyPropertyChanged
    {
        IEnumerable<MHome> homeModels;

        public HomeNotify() { }

        public HomeNotify(IEnumerable<MHome> homeModels)
        {
            this.homeModels = homeModels;
        }

        public IEnumerable<MHome> HomeModels
        { get { return homeModels; } set { this.MutateVerbose(ref homeModels, value, RaisePropertyChanged()); } }

        public event PropertyChangedEventHandler PropertyChanged;
        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
    }
}
