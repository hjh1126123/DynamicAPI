using ReportApp.ViewModel;
using Server;
using Server.DBLocal;
using System.Windows.Controls;

namespace ReportApp.Panel
{
    /// <summary>
    /// Setting.xaml 的交互逻辑
    /// </summary>
    public partial class PanelGroup : UserControl
    {
        readonly GroupViewModel GVM;
        BGroup checkedBox;
        public PanelGroup()
        {
            InitializeComponent();

            if (GVM == null)
            {
                GVM = new GroupViewModel();
            }

            DataContext = GVM;
        }

        private void BLLGroupLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            GVM.Groups = ServerKeeper.Instance.DBLocalKeeper.DBObject<B_Group>().SelectAll();
        }

        private void GroupList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lbx = (ListBox)sender;
            if (lbx.SelectedItem != null)
            {
                checkedBox = (BGroup)lbx.SelectedItem;
                GVM.Name = checkedBox.Gname;
                GVM.Describe = checkedBox.Gdescribe;
            }
        }

        private void Sumbit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            bool isComplete;
            if (checkedBox == null)
            {
                isComplete = ServerKeeper.Instance.DBLocalKeeper.DBObject<B_Group>().Add(new global::Server.DBLocal.Group
                {
                    Name = GVM.Name,
                    Describe = GVM.Describe
                });
            }
            else
            {
                isComplete = ServerKeeper.Instance.DBLocalKeeper.DBObject<B_Group>().Update(new global::Server.DBLocal.Group
                {
                    Id = checkedBox.Id,
                    Name = GVM.Name,
                    Describe = GVM.Describe
                });
            }
            if (isComplete)
            {
                GVM.Groups = ServerKeeper.Instance.DBLocalKeeper.DBObject<B_Group>().SelectAll();
                if (checkedBox != null)
                {
                    GroupList.SelectedItem = checkedBox;
                }
            }
        }

        private void Add_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            checkedBox = null;
            GVM.Name = "";
            GVM.Describe = "";
        }

        private void Delete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (checkedBox == null)
                return;

            if (ServerKeeper.Instance.DBLocalKeeper.DBObject<B_Group>().Delete(new global::Server.DBLocal.Group { Id = checkedBox.Id }))
            {
                GVM.Groups = ServerKeeper.Instance.DBLocalKeeper.DBObject<B_Group>().SelectAll();
            }
        }
    }
}
