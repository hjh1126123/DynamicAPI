using MaterialDesignThemes.Wpf;
using ServerApp.Pages;
using ServerApp.INotify;
using System;

namespace ServerApp.Model
{
    public class MainWindowViewModel
    {
        public RouterNotify[] RouterItems { get; }

        public MainWindowViewModel(ISnackbarMessageQueue snackbarMessageQueue)
        {
            if (snackbarMessageQueue == null) throw new ArgumentNullException(nameof(snackbarMessageQueue));            

            RouterItems = new[]
            {
                new RouterNotify("主页", new Home()),                
                new RouterNotify("服务", new ServerControl()),
                new RouterNotify("SQL维护", new SqlMaintenance()),
                new RouterNotify("主题修改",new PaletteSelector())
            };
        }
    }
}
