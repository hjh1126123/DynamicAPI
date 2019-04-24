using ReportApp.ViewModel;
using System.Windows.Controls;

namespace ReportApp.Panel
{
    /// <summary>
    /// PaletteSelector.xaml 的交互逻辑
    /// </summary>
    public partial class PanelPaletteSelector : UserControl
    {
        PaletteSelectorViewModel paletteSelectorViewModel;
        public PanelPaletteSelector()
        {
            InitializeComponent();

            if (paletteSelectorViewModel == null)
                paletteSelectorViewModel = new PaletteSelectorViewModel(Global.Instance.IsDark);

            DataContext = paletteSelectorViewModel;
        }
    }
}
