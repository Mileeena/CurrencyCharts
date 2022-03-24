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
using Avalonia.Controls.Selection;
using Avalonia.Controls;
using System;

namespace CurrencyCharts.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        List<string> TimeIntervals { get; set; } = new List<string> { "1m", "3m", "5m", "15m", "30m", "1h", "2h", "4h",
                                                                        "6h", "8h", "12h", "1d", "3d", "1w", "1M", };
        List<string> Currency { get; set; } = new List<string> { "BTCUSDT", "ETHUSDT", "BNBUSDT", "BSWUSDT", "XRPUSDT", "DOGEUSDT",
                                                                    "CAKEUSDT", "MINAUSDT","1INCHUSDT","BIFIUSDT"};
        public string symbol { get; set; } = "ETHUSDT";
        public string interval { get; set; } = "1m";
        public Binance binance { get; set; }

        private IEnumerable<ISeries> series;
        public IEnumerable<ISeries> Series 
        {   get => series;
            set
            {
                series = value;
                OnPropertyChanged(nameof(Series));
            }
        }
        private IEnumerable<ICartesianAxis> xAxes;
        public IEnumerable<ICartesianAxis> XAxes
        {
            get => xAxes;
            set
            {
                xAxes = value;
                OnPropertyChanged(nameof(XAxes));
            }
        }
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void NewChart()
        {
            binance = new Binance(symbol, interval);

            XAxes = new List<Axis>
            {
                    new Axis
                    {
                        LabelsRotation = 15,
                        //Labels = binance.candleTime,
                        Labeler = value => new DateTime((long)value).ToString("yyyy MMM dd"),
                        UnitWidth = binance.unitWidth.Ticks
                    }
            };

            Series = new ObservableCollection<ISeries>
                {
                     new CandlesticksSeries<FinancialPoint>
                     {
                        
                        Values = binance?.pricesList
                     }
                };
        }

        public MainWindowViewModel()
        {

        }
    }
}

//XAxes = new List<Axis>
//{
//    new Axis
//    {
//        Labeler = value => new DateTime((long)value).ToString("MM/dd/yy"),
//        UnitWidth = TimeSpan.FromDays(1).Ticks
//    }
//};

//Series = new ObservableCollection<ISeries>
//{
//    new LineSeries<DateTimePoint>
//    {
//        Name = "Close",
//        Values = _observableCloseValues,
//        Fill = null,
//        GeometrySize = 8,
//        TooltipLabelFormatter = point => $"Last: {point.Model!.Value:F2} on {point.Model!.DateTime.ToString("d")}"
//    },
//    new LineSeries<DateTimePoint>
//    {
//        Name = "14 Day SMA",
//        Values = _observableSma,
//        Fill = null,
//        GeometrySize = 4,
//        TooltipLabelFormatter = point => $"SMA: {point.Model!.Value:F2}"
//    }
//};