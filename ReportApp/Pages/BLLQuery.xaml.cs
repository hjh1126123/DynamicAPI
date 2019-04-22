using ReportApp.INotify;
using ReportApp.Model;
using System.Collections.Generic;
using System.Windows.Controls;
using System;

namespace ReportApp.Pages
{
    /// <summary>
    /// Setting.xaml 的交互逻辑
    /// </summary>
    public partial class BLLQuery : UserControl
    {
        BLLQueryNotify bLLQueryNotify;

        public BLLQuery()
        {
            InitializeComponent();

            if (bLLQueryNotify == null)
            {
                bLLQueryNotify = new BLLQueryNotify();
            }

            DataContext = bLLQueryNotify;
        }

        private void SQLMaintenanceLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            List<Model.MQuery.BoxItem> sqlType = new List<Model.MQuery.BoxItem>
                {
                    new Model.MQuery.BoxItem
                    {
                        Id = 0,
                        Text = "分组1"
                    },
                    new Model.MQuery.BoxItem
                    {
                        Id = 1,
                        Text = "分组2"
                    },
                    new Model.MQuery.BoxItem
                    {
                        Id = 2,
                        Text = "分组3"
                    },
                    new Model.MQuery.BoxItem
                    {
                        Id = 3,
                        Text = "分组4"
                    },
                    new Model.MQuery.BoxItem
                    {
                        Id = 4,
                        Text = "分组5"
                    },
                    new Model.MQuery.BoxItem
                    {
                        Id = 4,
                        Text = "分组6"
                    },
                    new Model.MQuery.BoxItem
                    {
                        Id = 4,
                        Text = "分组7"
                    },
                    new Model.MQuery.BoxItem
                    {
                        Id = 4,
                        Text = "分组8"
                    },
                    new Model.MQuery.BoxItem
                    {
                        Id = 4,
                        Text = "分组9"
                    },
                    new Model.MQuery.BoxItem
                    {
                        Id = 4,
                        Text = "分组10"
                    },
                    new Model.MQuery.BoxItem
                    {
                        Id = 4,
                        Text = "分组11"
                    }
                };
            List<Model.MQuery.BoxItem> sqlActive = new List<Model.MQuery.BoxItem>
                {
                    new Model.MQuery.BoxItem
                    {
                        Id = 0,
                        Text = "业务1"
                    },

                    new Model.MQuery.BoxItem
                    {
                        Id = 1,
                        Text = "业务2"
                    },
                    new Model.MQuery.BoxItem
                    {
                        Id = 2,
                        Text = "业务3"
                    },
                    new Model.MQuery.BoxItem
                    {
                        Id = 3,
                        Text = "业务4"
                    },
                    new Model.MQuery.BoxItem
                    {
                        Id = 4,
                        Text = "业务5"
                    }
                };
            bLLQueryNotify.BllQuery = new Model.MQuery(sqlType, sqlActive, "select * from all");
        }
    }
}
