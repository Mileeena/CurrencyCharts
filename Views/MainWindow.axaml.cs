using Avalonia.Controls;
using Avalonia.Controls.Selection;
using Avalonia.Markup.Xaml;
using CurrencyCharts.ViewModels;

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
        public void SelectionChanged(object? sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            ViewModel.NewChart();
        }

        MainWindowViewModel ViewModel => DataContext as MainWindowViewModel;
    }
}
