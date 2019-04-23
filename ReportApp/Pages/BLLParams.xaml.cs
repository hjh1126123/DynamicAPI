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
    public partial class BLLParams : UserControl
    {
        readonly ParamsNotify paramsNotify;
        BParam checkedBox;
        public BLLParams()
        {
            InitializeComponent();

            if (paramsNotify == null)
            {
                paramsNotify = new ParamsNotify();
            }

            DataContext = paramsNotify;
        }

        private void BLLGroupLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            paramsNotify.Params = DBKeeper.Instance.DBObject<B_Params>().SelectAll();
        }

        private void GroupList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lbx = (ListBox)sender;
            if (lbx.SelectedItem != null)
            {
                checkedBox = (BParam)lbx.SelectedItem;
                paramsNotify.Name = checkedBox.Name;
                paramsNotify.Describe = checkedBox.Describe;
                paramsNotify.Key = checkedBox.Key;
            }
        }

        private void Sumbit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            bool isComplete;
            if (checkedBox == null)
            {
                isComplete = DBKeeper.Instance.DBObject<B_Params>().Add(new Params
                {
                    Name = paramsNotify.Name,
                    Describe = paramsNotify.Describe,
                    Key = paramsNotify.Key
                });
            }
            else
            {
                isComplete = DBKeeper.Instance.DBObject<B_Params>().Update(new Params
                {
                    Id = checkedBox.Id,
                    Name = paramsNotify.Name,
                    Describe = paramsNotify.Describe,
                    Key = paramsNotify.Key
                });
            }
            if (isComplete)
            {
                paramsNotify.Params = DBKeeper.Instance.DBObject<B_Params>().SelectAll();
                if (checkedBox != null)
                {
                    ParamsList.SelectedItem = checkedBox;
                }
            }
        }

        private void Add_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            checkedBox = null;
            paramsNotify.Name = "";
            paramsNotify.Describe = "";
            paramsNotify.Key = "";
        }

        private void Delete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (checkedBox == null)
                return;

            if (DBKeeper.Instance.DBObject<B_Params>().Delete(new Params { Id = checkedBox.Id }))
            {
                paramsNotify.Params = DBKeeper.Instance.DBObject<B_Params>().SelectAll();
            }
        }
    }
}
