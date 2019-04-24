using PropertyChanged;
using System.Windows;
using System.Windows.Controls;

namespace ReportApp.ViewModel
{
    [ImplementPropertyChanged]
    public class RouterViewModel
    {
        public string Name { get; set; }
        public object Content { get; set; }
        public ScrollBarVisibility HorizontalScrollBarVisibilityRequirement { get; set; }
        public ScrollBarVisibility VerticalScrollBarVisibilityRequirement { get; set; }
        public Thickness MarginRequirement { get; set; }

        public RouterViewModel(string name, object content)
        {
            Name = name;
            Content = content;
            MarginRequirement = new Thickness(16);
        }
    }
}
