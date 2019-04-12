using MaterialDesignThemes.Wpf;
using ServerApp.Controls;
using ServerApp.Model;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace ServerApp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Snackbar Snackbar;

        public MainWindow()
        {
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(500);
            }).ContinueWith(t =>
            {
                MainSnackbar.MessageQueue.Enqueue("欢迎来到海带宝报表监控服务端控制器");
            }, TaskScheduler.FromCurrentSynchronizationContext());

            DataContext = new MMainWindow(MainSnackbar.MessageQueue);

            Snackbar = MainSnackbar;
        }

        private void NavListBox_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var dependencyObject = Mouse.Captured as DependencyObject;
            while (dependencyObject != null)
            {
                if (dependencyObject is ScrollBar) return;
                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            }

            MenuToggleButton.IsChecked = false;
        }       

        private void DragMove(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Minimized(object sender, RoutedEventArgs e)
        {
            if (WindowState != WindowState.Minimized)
                WindowState = WindowState.Minimized;
        }

        private void Maximized(object sender, RoutedEventArgs e)
        {
            if (WindowState != WindowState.Maximized)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
