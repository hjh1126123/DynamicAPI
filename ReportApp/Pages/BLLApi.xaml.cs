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
    public partial class BLLApi : UserControl
    {
        readonly ApiNotify apiNotify;
        IApi checkedBox;
        public BLLApi()
        {
            InitializeComponent();

            if (apiNotify == null)
            {
                apiNotify = new ApiNotify();
            }

            DataContext = apiNotify;
        }

        private void BLLGroupLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            apiNotify.Apis = DBKeeper.Instance.DBObject<I_Api>().SelectAll();
            apiNotify.Charts = new List<string>
            {
                "bar",
                "circle",
                "other"
            };
            apiNotify.Pattern = new List<string>
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
                apiNotify.Name = checkedBox.Apiname;
                apiNotify.Describe = checkedBox.Apidescribe;
                ChartCombox.SelectedItem = checkedBox.Chart;
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
                    ApiName = apiNotify.Name,
                    ApiDescribe = apiNotify.Describe,
                    RequestKey = apiNotify.RequestKey,
                    Pattern = PatternCombox.SelectedItem.ToString(),
                    Chart = ChartCombox.SelectedItem.ToString()
                });
            }
            else
            {
                isComplete = DBKeeper.Instance.DBObject<I_Api>().Update(new Api
                {
                    Id = checkedBox.Id,
                    ApiName = apiNotify.Name,
                    ApiDescribe = apiNotify.Describe,
                    RequestKey = apiNotify.RequestKey,
                    Chart = ChartCombox.SelectedItem.ToString(),
                    Pattern = PatternCombox.SelectedItem.ToString()
                });
            }
            if (isComplete)
            {
                apiNotify.Apis = DBKeeper.Instance.DBObject<I_Api>().SelectAll();
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
            apiNotify.Name = "";
            apiNotify.Describe = "";
            ChartCombox.SelectedItem = null;
        }

        private void Delete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (checkedBox == null)
                return;

            if (DBKeeper.Instance.DBObject<I_Api>().Delete(new Api { Id = checkedBox.Id }))
            {
                apiNotify.Apis = DBKeeper.Instance.DBObject<I_Api>().SelectAll();
            }
        }
    }
}
