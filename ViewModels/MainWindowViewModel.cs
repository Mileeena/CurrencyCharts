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

    //�    1m     // 1 ������
    //�    3m     // 3 ������
    //�    5m    // 5 �����
    //�    15m  // 15 �����
    //�    30m    // 30 �����
    //�    1h    // 1 ���
    //�    2h    // 2 ����
    //�    4h    // 4 ����
    //�    6h    // 6 �����
    //�    8h    // 8 �����
    //�    12h    // 12 �����
    //�    1d    // 1 ����
    //�    3d    // 3 ���
    //�    1w    // 1 ������
    //�    1M    // 1 �����