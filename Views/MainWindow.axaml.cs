using System;
using System.Collections.Generic;
using System.Drawing;
using Avalonia.Controls;
using CurrencyCharts.Models;
using CurrencyCharts.ViewModels;
using ScottPlot;
using ScottPlot.Avalonia;

namespace CurrencyCharts.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //string symbol = "ETHUSDT";
            //string interval = "15m";
            //List<OHLC> pricesList = new Binance(symbol, interval).pricesList;

            //var candlePlot = Chart.Plot.AddCandlesticks(pricesList.ToArray());

            //Chart.Plot.XAxis.DateTimeFormat(true);
            //Chart.Plot.XAxis.PixelSnap(true);
            //Chart.Plot.XAxis.RulerMode(true);

            ////
            //Chart.Plot.Layout(padding: 12);
            //Chart.Plot.Style(figureBackground: Color.White, dataBackground: ColorTranslator.FromHtml("#151a1e"));
            //Chart.Plot.XAxis.TickLabelStyle(color: Color.Black);
            //Chart.Plot.XAxis.TickMarkColor(Color.Black);
            //Chart.Plot.XAxis.MajorGrid(color: ColorTranslator.FromHtml("#191e23"));

            //Chart.Plot.YAxis.Grid(false);
            //Chart.Plot.YAxis2.Grid(true);
            //Chart.Plot.YAxis2.TickLabelStyle(color: Color.Black);
            //Chart.Plot.YAxis2.TickMarkColor(Color.Black);
            //Chart.Plot.YAxis2.MajorGrid(color: ColorTranslator.FromHtml("#191e23"));

            //candlePlot.ColorDown = ColorTranslator.FromHtml("#ff005b");
            //candlePlot.ColorUp = ColorTranslator.FromHtml("#00d387");

            //Chart.Refresh();
            //Chart.Plot.SaveFig("finance_dateTimeAxis.png");
        }
    }
}
