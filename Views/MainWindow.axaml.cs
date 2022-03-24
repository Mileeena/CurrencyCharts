using Avalonia.Controls;
using Avalonia.Controls.Selection;
using Avalonia.Markup.Xaml;

namespace CurrencyCharts.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public void SelectionChanged()
        {
            
        }
    }
}
