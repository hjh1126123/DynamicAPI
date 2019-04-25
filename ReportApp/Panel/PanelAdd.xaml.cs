using ReportApp.ViewModel;
using Server;
using Server.DBLocal;
using Server.Strategy;
using System.Collections.Generic;
using System.Windows.Controls;

namespace ReportApp.Panel
{
    /// <summary>
    /// Setting.xaml 的交互逻辑
    /// </summary>
    public partial class PanelAdd : UserControl
    {
        readonly AddViewModel addVM;
        public PanelAdd()
        {
            InitializeComponent();

            if (addVM == null)
            {
                addVM = new AddViewModel();
            }

            DataContext = addVM;
        }

        private void BllAddLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            addVM.Groups = ServerKeeper.Instance.DBLocalKeeper.DBObject<B_Group>().SelectAll();
            addVM.Apis = ServerKeeper.Instance.DBLocalKeeper.DBObject<I_Api>().SelectAll();

            addVM.Params = new System.ComponentModel.BindingList<CheckBoxParams>();
            var listP = ServerKeeper.Instance.DBLocalKeeper.DBObject<B_Params>().SelectAll();
            foreach (var i in listP)
            {
                addVM.Params.Add(new CheckBoxParams
                {
                    Checked = false,
                    BParam = i
                });
            }
            List<StrategyModel> strategyModels = new List<StrategyModel>();
            foreach(var key in ServerKeeper.Instance.StrategyKeeper.Strategys.Keys)
            {
                strategyModels.Add(ServerKeeper.Instance.StrategyKeeper.Strategys[key]);
            }
            addVM.Strategys = strategyModels;
        }

        private void Sumbit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            BGroup group = (BGroup)GroupCombox.SelectedItem;
            IApi api = (IApi)LibCombox.SelectedItem;
            StrategyModel strategyModel = (StrategyModel)StrategyCombobox.SelectedItem;
            if (group == null || api == null || strategyModel == null)
                return;

            var active = ServerKeeper.Instance.DBLocalKeeper.DBObject<B_Active>().Add(new Active
            {
                Gid = group.Gid,
                Name = addVM.ActiveName,
                Describe = addVM.ActiveDescribe
            });

            List<string> paramsFormat = new List<string>();
            foreach(var item in addVM.Params)
            {
                if (item.Checked)
                    paramsFormat.Add(item.BParam.Pid);
            }
            string @params = string.Empty;
            for (int pCount = 0; pCount < paramsFormat.Count; pCount++)
            {
                @params += paramsFormat[pCount];
                if (pCount < paramsFormat.Count - 1)
                    @params += ",";
            }
            ServerKeeper.Instance.DBLocalKeeper.DBObject<D_MsSQL>().Add(new MsSQL
            {
                Aid = active.Aid,
                ApiId = api.Apiid,
                Paramskey = @params,
                Strategy = strategyModel.Name,
                Sql = addVM.Sql
            });
        }
    }
}
