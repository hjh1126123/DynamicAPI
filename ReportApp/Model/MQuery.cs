using ReportApp.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ReportApp.Model
{
    public class MQuery
    {
        public struct BoxItem
        {
            public int Id { get; set; }
            public string Text { get; set; }
        }
        
        public ObservableCollection<BoxItem> SqlGroup { get ; set; }
        public ObservableCollection<BoxItem> SqlActive { get; set; }
        public string Sql { get; set; }

        public MQuery(List<BoxItem> sqlGroup, List<BoxItem> sqlActive, string sql)
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
