using ReportApp.Panel;
using ReportApp.ViewModel;

namespace ReportApp.ViewModel
{
    public class MainWindowViewModel
    {
        public RouterViewModel[] RouterItems { get; }
        public MainWindowViewModel()
        {            
            RouterItems = new[]
            {
                new RouterViewModel("主页", new PanelHome()),
                new RouterViewModel("服务", new PanelServe()),
                new RouterViewModel("业务添加", new PanelAdd()),
                new RouterViewModel("业务查看", new PanelQuery()),
                new RouterViewModel("业务组管理", new PanelGroup()),
                new RouterViewModel("参数管理", new PanelParams()),
                new RouterViewModel("接口管理", new PanelApi()),
                new RouterViewModel("主题修改",new PanelPaletteSelector())
            };
        }
    }
}
