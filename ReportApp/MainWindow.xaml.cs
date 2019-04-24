using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using ReportApp.ViewModel;
using Server.Local;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace ReportApp
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

            //位置
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            //公共组件
            Global.Instance.TheMessageBox = MainSnackbar;

            //设定主题
            UTheme uTheme = DBKeeper.Instance.DBObject<U_Theme>().Select();
            PaletteHelper paletteHelper = new PaletteHelper();
            if (uTheme.Isdark != null)
            {                
                paletteHelper.SetLightDark(uTheme.Isdark.GetValueOrDefault());
                Global.Instance.IsDark = uTheme.Isdark.GetValueOrDefault();
            }                
            if (!string.IsNullOrWhiteSpace(uTheme.Primary))
            {
                paletteHelper.ReplacePrimaryColor(JsonConvert.DeserializeObject<Swatch>(uTheme.Primary));
            }
            if (!string.IsNullOrWhiteSpace(uTheme.Accent))
            {
                paletteHelper.ReplaceAccentColor(JsonConvert.DeserializeObject<Swatch>(uTheme.Accent));
            }


            DataContext = new MainWindowViewModel();
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
