using System.Windows.Controls;

namespace ReportApp.Panel
{
    /// <summary>
    /// Setting.xaml 的交互逻辑
    /// </summary>
    public partial class PanelQuery : UserControl
    {
        public PanelQuery()
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
