using ReportApp.ViewModel;
using Server.Local;
using System.Collections.Generic;
using System.Windows.Controls;

namespace ReportApp.Panel
{
    /// <summary>
    /// Setting.xaml 的交互逻辑
    /// </summary>
    public partial class PanelApi : UserControl
    {
        readonly ApiViewModel apiVM;
        IApi checkedBox;
        public PanelApi()
        {
            InitializeComponent();

            if (apiVM == null)
            {
                apiVM = new ApiViewModel();
            }

            DataContext = apiVM;
        }

        private void BLLGroupLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            apiVM.Apis = DBKeeper.Instance.DBObject<I_Api>().SelectAll();
            apiVM.Charts = new List<string>
            {
                "bar",
                "circle",
                "other"
            };
            apiVM.Pattern = new List<string>
            {
                "即时查询",
                "缓存查询"
            };
        }

        private void GroupList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lbx = (ListBox)sender;
            if (lbx.SelectedItem != null)
            {
                checkedBox = (IApi)lbx.SelectedItem;
                apiVM.Name = checkedBox.Apiname;
                apiVM.Describe = checkedBox.Apidescribe;
                ChartCombox.SelectedItem = checkedBox.Chart;
                PatternCombox.SelectedItem = checkedBox.Pattern;
            }
        }

        private void Sumbit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ChartCombox.SelectedItem == null || PatternCombox.SelectedItem == null)
            {
                Global.Instance.ShowMessage("接口指向图表或接口数据查询模式未选择");
                return;
            }
                

            bool isComplete;
            if (checkedBox == null)
            {
                isComplete = DBKeeper.Instance.DBObject<I_Api>().Add(new Api
                {
                    ApiName = apiVM.Name,
                    ApiDescribe = apiVM.Describe,
                    RequestKey = apiVM.RequestKey,
                    Pattern = PatternCombox.SelectedItem.ToString(),
                    Chart = ChartCombox.SelectedItem.ToString()
                });
            }
            else
            {
                isComplete = DBKeeper.Instance.DBObject<I_Api>().Update(new Api
                {
                    Id = checkedBox.Id,
                    ApiName = apiVM.Name,
                    ApiDescribe = apiVM.Describe,
                    RequestKey = apiVM.RequestKey,
                    Chart = ChartCombox.SelectedItem.ToString(),
                    Pattern = PatternCombox.SelectedItem.ToString()
                });
            }
            if (isComplete)
            {
                apiVM.Apis = DBKeeper.Instance.DBObject<I_Api>().SelectAll();
                if (checkedBox != null)
                {
                    ApiList.SelectedItem = checkedBox;
                }
                Global.Instance.ShowMessage("数据提交成功");
            }
            else
            {
                Global.Instance.ShowMessage("数据提交失败，详情请看日志");
            }
        }

        private void Add_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            checkedBox = null;
            apiVM.Name = "";
            apiVM.Describe = "";
            ChartCombox.SelectedItem = null;
        }

        private void Delete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (checkedBox == null)
                return;

            if (DBKeeper.Instance.DBObject<I_Api>().Delete(new Api { Id = checkedBox.Id }))
            {
                apiVM.Apis = DBKeeper.Instance.DBObject<I_Api>().SelectAll();
            }
        }
    }
}
