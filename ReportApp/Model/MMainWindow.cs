using MaterialDesignThemes.Wpf;
using ReportApp.INotify;
using ReportApp.Pages;
using System;

namespace ReportApp.Model
{
    public class MMainWindow
    {
        public RouterNotify[] RouterItems { get; }
        public MMainWindow()
        {            
            RouterItems = new[]
            {
                new RouterNotify("主页", new Home()),
                new RouterNotify("服务", new ServeControl()),
                new RouterNotify("业务添加", new BLLAdd()),
                new RouterNotify("业务查看", new BLLQuery()),
                new RouterNotify("业务组管理", new BLLGroup()),
                new RouterNotify("参数管理", new BLLParams()),
                new RouterNotify("接口管理", new BLLApi()),
                new RouterNotify("主题修改",new PaletteSelector())
            };
        }
    }
}
