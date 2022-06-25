using System.Collections.ObjectModel;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.Kernel.Sketches;
using CurrencyCharts.Data;
using PropertyChanged;

namespace CurrencyCharts.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class MainWindowViewModel
    {

        List<string> TimeIntervals { get; set; } = new List<string> { "1m", "3m", "5m", "15m", "30m", "1h", "2h", "4h",
                                                                        "6h", "8h", "12h", "1d", "3d", "1w", "1M", };
        List<string> Currency { get; set; } = new List<string> { "BTCUSDT", "ETHUSDT", "BNBUSDT", "BSWUSDT", "XRPUSDT", "DOGEUSDT",
                                                                    "CAKEUSDT", "MINAUSDT","1INCHUSDT","BIFIBUSD"};
        public Binance binance { get; set; }

        private string symbol;
        private string interval;

        public string Symbol
        {
            get => symbol;
            set
            {
                symbol = value;
                if (Interval != null)
                {
                    NewChartAsync();
                }
            }
        }
        public string Interval
        {
            get => interval;
            set
            {
                interval = value;
                if (Interval != null)
                {
                    NewChartAsync();
                }
            }
        }

        public IEnumerable<ISeries> Series { get; set; }
        public IEnumerable<ICartesianAxis> XAxes { get; set; }

        public void NewChart()
        {
            binance = new Binance(Symbol, Interval);
            XAxes = new List<Axis>
            {
                new Axis
                {
                    LabelsRotation = 15,
                    Labeler = value => "",
                    //Labeler = value => new DateTime((long)value).ToString("yyyy MMM dd"),
                    UnitWidth = binance.unitWidth.Ticks
                }
            };

            Series = new ObservableCollection<ISeries>
            {
                new CandlesticksSeries<FinancialPoint>
                {
                    Values = binance.pricesList
                }
            };
        }

        async Task NewChartAsync()
        {
            await Task.Run(() => NewChart());
        }
        public MainWindowViewModel()
        {
            symbol = "BTCUSDT";
            interval = "5m";
            NewChart();
        }
    }
}