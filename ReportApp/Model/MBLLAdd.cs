using ServerApp.Command;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using EntityLocal;

namespace ServerApp.Model
{
    public class MBLLAdd
    {
        public ObservableCollection<BGroup> Group { get; set; }
        public string Active { get; set; }
        public string SQL { get; set; }

        public MBLLAdd(List<BGroup> group)
        {
            Group = new ObservableCollection<BGroup>(group);
        }

        public ICommand ChangeGroupAction
        {
            get
            {
                return new AnotherCommandImplementation(o => ChangeGroup(o));
            }
        }
        private void ChangeGroup(object o)
        {
            
        }
    }
}
