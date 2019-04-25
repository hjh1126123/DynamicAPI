using ReportApp.ViewModel;
using Server;
using Server.DBLocal;
using System.Windows.Controls;

namespace ReportApp.Panel
{
    /// <summary>
    /// Setting.xaml 的交互逻辑
    /// </summary>
    public partial class PanelParams : UserControl
    {
        readonly ParamsViewModel PVM;
        BParam checkedBox;
        public PanelParams()
        {
            InitializeComponent();

            if (PVM == null)
            {
                PVM = new ParamsViewModel();
            }

            DataContext = PVM;
        }

        private void BLLGroupLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            PVM.Params = ServerKeeper.Instance.DBLocalKeeper.DBObject<B_Params>().SelectAll();
        }

        private void GroupList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lbx = (ListBox)sender;
            if (lbx.SelectedItem != null)
            {
                checkedBox = (BParam)lbx.SelectedItem;
                PVM.Name = checkedBox.Name;
                PVM.Describe = checkedBox.Describe;
                PVM.Key = checkedBox.Key;
                PVM.Multiple = checkedBox.Multiple.GetValueOrDefault();
            }
        }

        private void Sumbit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            bool isComplete;
            if (checkedBox == null)
            {
                isComplete = ServerKeeper.Instance.DBLocalKeeper.DBObject<B_Params>().Add(new Params
                {
                    Name = PVM.Name,
                    Describe = PVM.Describe,
                    Key = PVM.Key,
                    Multiple = PVM.Multiple
                });
            }
            else
            {
                isComplete = ServerKeeper.Instance.DBLocalKeeper.DBObject<B_Params>().Update(new Params
                {
                    Id = checkedBox.Id,
                    Name = PVM.Name,
                    Describe = PVM.Describe,
                    Key = PVM.Key,
                    Multiple = PVM.Multiple
                });
            }
            if (isComplete)
            {
                PVM.Params = ServerKeeper.Instance.DBLocalKeeper.DBObject<B_Params>().SelectAll();
                if (checkedBox != null)
                {
                    ParamsList.SelectedItem = checkedBox;
                }
            }
        }

        private void Add_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            checkedBox = null;
            PVM.Name = "";
            PVM.Describe = "";
            PVM.Key = "";
            PVM.Multiple = false;
        }

        private void Delete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (checkedBox == null)
                return;

            if (ServerKeeper.Instance.DBLocalKeeper.DBObject<B_Params>().Delete(new Params { Id = checkedBox.Id }))
            {
                PVM.Params = ServerKeeper.Instance.DBLocalKeeper.DBObject<B_Params>().SelectAll();
            }
        }

        private void Flipper_OnIsFlippedChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<bool> e)
        {

        }
    }
}
