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
        public BLLQuery()
        {
            InitializeComponent();

            //if (bLLQueryNotify == null)
            //{
            //    bLLQueryNotify = new BLLQueryNotify();
            //}

            //DataContext = bLLQueryNotify;
        }

        private void SQLMaintenanceLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            
        }
    }
}
