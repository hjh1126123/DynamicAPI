using MaterialDesignThemes.Wpf;
using ServerApp.Pages;
using ServerApp.INotify;
using System;

namespace ServerApp.Model
{
    public class MMainWindow
    {
        public RouterNotify[] RouterItems { get; }

        public MMainWindow(ISnackbarMessageQueue snackbarMessageQueue)
        {
            if (snackbarMessageQueue == null) throw new ArgumentNullException(nameof(snackbarMessageQueue));            

            RouterItems = new[]
            {
                new RouterNotify("主页", new Home()),
                new RouterNotify("服务", new Server()),
                new RouterNotify("业务添加", new BLLAdd()),
                //new RouterNotify("业务查看", new MBLLQuery()),
                new RouterNotify("主题修改",new PaletteSelector())
            };
        }
    }
}
