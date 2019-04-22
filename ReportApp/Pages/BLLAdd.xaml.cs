using ReportApp.INotify;
using Server;
using Server.Local;
using System.Collections.Generic;
using System.Windows.Controls;
using EntityLocal;

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
            addNotify.Libs = new List<string>
            {
                "bar",
                "circle"
            };
            addNotify.Params = DBKeeper.Instance.DBObject<B_Params>().SelectAll();
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
                Group = group.Gid,
                Active = active.Aid,
                Sql = addNotify.Sql                
            });

            //添加 pid
        }
    }
}
