using EntityLocal;
using ReportApp.INotify;
using Server;
using Server.Local;
using System.Windows.Controls;

namespace ReportApp.Pages
{
    /// <summary>
    /// Setting.xaml 的交互逻辑
    /// </summary>
    public partial class BLLGroup : UserControl
    {
        readonly GroupNotify notifyGroup;
        BGroup checkedBox;
        public BLLGroup()
        {
            InitializeComponent();

            if (notifyGroup == null)
            {
                notifyGroup = new GroupNotify();
            }

            DataContext = notifyGroup;
        }

        private void BLLGroupLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            notifyGroup.Groups = DBKeeper.Instance.DBObject<B_Group>().SelectAll();
        }

        private void GroupList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lbx = (ListBox)sender;
            if (lbx.SelectedItem != null)
            {
                checkedBox = (BGroup)lbx.SelectedItem;
                notifyGroup.Name = checkedBox.Gname;
                notifyGroup.Describe = checkedBox.Gdescribe;
            }
        }

        private void Sumbit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            bool isComplete;
            if (checkedBox == null)
            {
                isComplete = DBKeeper.Instance.DBObject<B_Group>().Add(new global::Server.Local.Group
                {
                    Name = notifyGroup.Name,
                    Describe = notifyGroup.Describe
                });
            }
            else
            {
                isComplete = DBKeeper.Instance.DBObject<B_Group>().Update(new global::Server.Local.Group
                {
                    Id = checkedBox.Id,
                    Name = notifyGroup.Name,
                    Describe = notifyGroup.Describe
                });
            }
            if (isComplete)
            {
                notifyGroup.Groups = DBKeeper.Instance.DBObject<B_Group>().SelectAll();
                if (checkedBox != null)
                {
                    GroupList.SelectedItem = checkedBox;
                }
            }
        }

        private void Add_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            checkedBox = null;
            notifyGroup.Name = "";
            notifyGroup.Describe = "";
        }

        private void Delete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (checkedBox == null)
                return;

            if (DBKeeper.Instance.DBObject<B_Group>().Delete(new global::Server.Local.Group { Id = checkedBox.Id }))
            {
                notifyGroup.Groups = DBKeeper.Instance.DBObject<B_Group>().SelectAll();
            }
        }
    }
}
