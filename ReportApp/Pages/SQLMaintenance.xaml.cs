using ServerApp.INotify;
using ServerApp.Model;
using System.Collections.Generic;
using System.Windows.Controls;

namespace ServerApp.Pages
{
    /// <summary>
    /// Setting.xaml 的交互逻辑
    /// </summary>
    public partial class SQLMaintenance : UserControl
    {
        SQLMaintenanceNotify SQLMaintenanceNotify;

        public SQLMaintenance()
        {
            InitializeComponent();

            if (SQLMaintenanceNotify == null)
            {
                SQLMaintenanceNotify = new SQLMaintenanceNotify();
            }

            DataContext = SQLMaintenanceNotify;
        }

        private void SQLMaintenanceLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            List<MSQLMaintenance.BoxItem> sqlType = new List<MSQLMaintenance.BoxItem>
                {
                    new MSQLMaintenance.BoxItem
                    {
                        Id = 0,
                        Text = "分组1"
                    },
                    new MSQLMaintenance.BoxItem
                    {
                        Id = 1,
                        Text = "分组2"
                    },
                    new MSQLMaintenance.BoxItem
                    {
                        Id = 2,
                        Text = "分组3"
                    },
                    new MSQLMaintenance.BoxItem
                    {
                        Id = 3,
                        Text = "分组4"
                    },
                    new MSQLMaintenance.BoxItem
                    {
                        Id = 4,
                        Text = "分组5"
                    }
                };
            List<MSQLMaintenance.BoxItem> sqlActive = new List<MSQLMaintenance.BoxItem>
                {
                    new MSQLMaintenance.BoxItem
                    {
                        Id = 0,
                        Text = "业务1"
                    },

                    new MSQLMaintenance.BoxItem
                    {
                        Id = 1,
                        Text = "业务2"
                    },
                    new MSQLMaintenance.BoxItem
                    {
                        Id = 2,
                        Text = "业务3"
                    },
                    new MSQLMaintenance.BoxItem
                    {
                        Id = 3,
                        Text = "业务4"
                    },
                    new MSQLMaintenance.BoxItem
                    {
                        Id = 4,
                        Text = "业务5"
                    }
                };

            SQLMaintenanceNotify.SQLMaintenanceModel = new MSQLMaintenance(sqlType, sqlActive, "select * from all");
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
