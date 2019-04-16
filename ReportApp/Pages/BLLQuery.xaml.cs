using ServerApp.INotify;
using ServerApp.Model;
using System.Collections.Generic;
using System.Windows.Controls;
using System;

namespace ServerApp.Pages
{
    /// <summary>
    /// Setting.xaml 的交互逻辑
    /// </summary>
    public partial class BLLQuery : UserControl
    {
        SQLMaintenanceNotify SQLMaintenanceNotify;

        public BLLQuery()
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
                    },
                    new MSQLMaintenance.BoxItem
                    {
                        Id = 4,
                        Text = "分组6"
                    },
                    new MSQLMaintenance.BoxItem
                    {
                        Id = 4,
                        Text = "分组7"
                    },
                    new MSQLMaintenance.BoxItem
                    {
                        Id = 4,
                        Text = "分组8"
                    },
                    new MSQLMaintenance.BoxItem
                    {
                        Id = 4,
                        Text = "分组9"
                    },
                    new MSQLMaintenance.BoxItem
                    {
                        Id = 4,
                        Text = "分组10"
                    },
                    new MSQLMaintenance.BoxItem
                    {
                        Id = 4,
                        Text = "分组11"
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
    }
}
