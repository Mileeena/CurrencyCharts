using System.Collections.Generic;
using System.Drawing;
using Avalonia.Controls;
using CurrencyCharts.Models;
using CurrencyCharts.Views;
using ScottPlot;
using ScottPlot.Avalonia;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;

namespace CurrencyCharts.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        // we have to let the chart know that the X axis in days.
        public static Binance binance { get; set; } = new Binance("ETHUSDT", "15m");
        public Axis[] XAxes { get; set; } = {
            new()
            {
                LabelsRotation = 15,
                Labeler = value => new DateTime((long)value).ToString("yyyy MMM dd"),
                // set the unit width of the axis to "days"
                // since our X axis is of type date time and 
                // the interval between our points is in days
                UnitWidth = binance.unitWidth.Ticks
            }
        };
        public string symbol = "ETHUSDT";
        public string interval = "15m";
        public IEnumerable<ISeries> Series { get; set; } = new ObservableCollection<ISeries>
        {
            new CandlesticksSeries<FinancialPoint>
            {
                Values = binance?.pricesList
            }
        };
        public MainWindowViewModel()
        {

        }
    }
}

    //Х    1m     // 1 минута
    //Х    3m     // 3 минуты
    //Х    5m    // 5 минут
    //Х    15m  // 15 минут
    //Х    30m    // 30 минут
    //Х    1h    // 1 час
    //Х    2h    // 2 часа
    //Х    4h    // 4 часа
    //Х    6h    // 6 часов
    //Х    8h    // 8 часов
    //Х    12h    // 12 часов
    //Х    1d    // 1 день
    //Х    3d    // 3 дн€
    //Х    1w    // 1 недел€
    //Х    1M    // 1 мес€ц