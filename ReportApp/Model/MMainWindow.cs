using MaterialDesignThemes.Wpf;
using ReportApp.INotify;
using ReportApp.Pages;
using System;

namespace ReportApp.Model
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
                new RouterNotify("服务", new Pages.Server()),
                new RouterNotify("业务添加", new BLLAdd()),
                new RouterNotify("业务查看", new BLLQuery()),
                new RouterNotify("业务组管理", new BLLGroup()),
                new RouterNotify("主题修改",new PaletteSelector())
            };
        }
    }
}
