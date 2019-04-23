using EntityLocal;
using ReportApp.INotify;
using Server;
using Server.Local;
using System.Windows.Controls;
using System.Collections.Generic;

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
            IApi api = (IApi)LibCombox.SelectedItem;
            if (group == null && api == null)
                return;

            var active = DBKeeper.Instance.DBObject<B_Active>().Add(new Active
            {
                Gid = group.Gid,
                Name = addNotify.ActiveName,
                Describe = addNotify.ActiveDescribe
            });

            List<string> paramsFormat = new List<string>();
            foreach(var item in addNotify.Params)
            {
                if (item.Checked)
                    paramsFormat.Add(item.BParam.Key);
            }
            string @params = string.Empty;
            for (int pCount = 0; pCount < paramsFormat.Count; pCount++)
            {
                @params += paramsFormat[pCount];
                if (pCount < paramsFormat.Count - 1)
                    @params += ",";
            }
            DBKeeper.Instance.DBObject<D_MsSQL>().Add(new MsSQL
            {
                Aid = active.Aid,
                ApiKey = api.Apikey,
                Paramskey = @params,
                
                Sql = addNotify.Sql
            });
        }
    }
}
