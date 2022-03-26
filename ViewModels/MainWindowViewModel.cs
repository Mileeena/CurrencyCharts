using System.Collections.Generic;
using CurrencyCharts.Models;
using System.Collections.ObjectModel;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.Kernel.Sketches;
using System.Windows.Input;
using ReactiveUI;
using System.ComponentModel;

namespace CurrencyCharts.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ICommand Click { get; set; }

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
                OnPropertyChanged(nameof(Symbol));
                if (Interval != null)
                {
                    Click.Execute(null);
                }
            }
        }
        public string Interval
        {
            get => interval;
            set
            {
                interval = value;
                OnPropertyChanged(nameof(Interval));
                if (Interval != null)
                {
                    Click.Execute(null);
                }
            }
        }

        private IEnumerable<ISeries> series;
        private IEnumerable<ICartesianAxis> xAxes;

        public IEnumerable<ISeries> Series 
        {   get => series;
            set
            {
                series = value;
                OnPropertyChanged(nameof(Series));
            }
        }
        public IEnumerable<ICartesianAxis> XAxes
        {
            get => xAxes;
            set
            {
                xAxes = value;
                OnPropertyChanged(nameof(XAxes));
            }
        }

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

        public MainWindowViewModel()
        {
            symbol = "BTCUSDT";
            interval = "5m";
            NewChart();
            Click = ReactiveCommand.Create(() =>
            {
                NewChart();
            });
        }
    }
}