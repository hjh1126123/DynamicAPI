using ServerApp.Command;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace ServerApp.Model
{
    public class MBLLQuery
    {
        public struct BoxItem
        {
            public int Id { get; set; }
            public string Text { get; set; }
        }
        
        public ObservableCollection<BoxItem> SqlGroup { get ; set; }
        public ObservableCollection<BoxItem> SqlActive { get; set; }
        public string Sql { get; set; }

        public MBLLQuery(List<BoxItem> sqlGroup, List<BoxItem> sqlActive, string sql)
        {

            SqlGroup = new ObservableCollection<BoxItem>(sqlGroup);
            SqlActive = new ObservableCollection<BoxItem>(sqlActive);
            Sql = sql;
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
            SqlActive.Clear();
        }

        public ICommand ChangeActiveAction
        {
            get
            {
                return new AnotherCommandImplementation(o => ChangeActive(o));
            }
        }
        private void ChangeActive(object o)
        {
            
        }
    }
}
