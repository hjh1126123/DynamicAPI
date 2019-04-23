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
    public partial class BLLAdd : UserControl
    {
        readonly AddNotify addNotify;
        public BLLAdd()
        {
            InitializeComponent();

            if (addNotify == null)
            {
                addNotify = new AddNotify();
            }

            DataContext = addNotify;
        }

        private void BllAddLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            addNotify.Groups = DBKeeper.Instance.DBObject<B_Group>().SelectAll();
            addNotify.Apis = DBKeeper.Instance.DBObject<I_Api>().SelectAll();

            addNotify.Params = new System.ComponentModel.BindingList<CheckBoxParams>();
            var listP = DBKeeper.Instance.DBObject<B_Params>().SelectAll();
            foreach (var i in listP)
            {
                addNotify.Params.Add(new CheckBoxParams
                {
                    Checked = false,
                    BParam = i
                });
            }
        }

        private void Sumbit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            BGroup group = (BGroup)GroupCombox.SelectedItem;
            if (group == null)
                return;

            var active = DBKeeper.Instance.DBObject<B_Active>().Add(new Active
            {
                Gid = group.Gid,
                Name = addNotify.ActiveName,
                Describe = addNotify.ActiveDescribe
            });

            DBKeeper.Instance.DBObject<D_MsSQL>().Add(new MsSQL
            {
                Aid = active.Aid,
                Sql = addNotify.Sql
            });
        }
    }
}
