using ServerApp.INotify;
using ServerApp.Model;
using System.Collections.Generic;
using System.Windows.Controls;

namespace ServerApp.Pages
{
    /// <summary>
    /// ServeControl.xaml 的交互逻辑
    /// </summary>
    public partial class ServerControl : UserControl
    {
        ServerNotify serverCardItem;

        public ServerControl()
        {
            InitializeComponent();

            if (serverCardItem == null)
                serverCardItem = new ServerNotify();

            DataContext = serverCardItem;
        }

        private void ServerControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            serverCardItem.ServerControls = new List<ServerControlModel>
            {
                 // new ServerControlModel(
                 //   "pack://application:,,,/Resources/Img/Wcf.png",
                 //   "运行",
                 //   "Wcf服务进程",
                 //   "详情如下：\n\n" +
                 //   "1.默认端口1520，请确认无占用\n" +
                 //   "2.请确保拥有管理员权限\n" +
                 //   "3.使用TCP协议",
                 //   "WCF配置",()=>
                 //   {
                 //       return true;
                 //   },
                 //   new WcfServer()),
                 //new ServerControlModel(
                 //   "pack://application:,,,/Resources/Img/Quartz.png",
                 //   "运行",
                 //   "作业调度进程",
                 //   "详情如下：\n\n" +
                 //   "1.默认运行时间8:30\n" +
                 //   "2.默认缓存目录为cache\n",
                 //   "WCF配置",
                 //   ()=>
                 //   {
                 //       return QuartzControl.Instance.IsStarted;
                 //   },
                 //   new QuartzServer())
            };        
        }

        private void ServerControl_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            foreach (var serverModel in serverCardItem.ServerControls)
            {
                serverModel.Dispose();
            }
        }
    }
}
