using System.Collections.Generic;
using System.Drawing;
using Avalonia.Controls;
using CurrencyCharts.Models;
using CurrencyCharts.Views;
using ScottPlot;
using ScottPlot.Avalonia;

namespace CurrencyCharts.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public AvaPlot Chart { get; set; } = new AvaPlot();
        public MainWindowViewModel()
        {
            string symbol = "ETHUSDT";
            string interval = "15m";
            List<OHLC> pricesList = new Binance(symbol, interval).pricesList;

            var candlePlot = Chart.Plot.AddCandlesticks(pricesList.ToArray());

            Chart.Plot.XAxis.DateTimeFormat(true);
            Chart.Plot.XAxis.PixelSnap(true);
            Chart.Plot.XAxis.RulerMode(true);

            //
            Chart.Plot.Layout(padding: 12);
            Chart.Plot.Style(figureBackground: Color.White, dataBackground: ColorTranslator.FromHtml("#151a1e"));
            Chart.Plot.XAxis.TickLabelStyle(color: Color.Black);
            Chart.Plot.XAxis.TickMarkColor(Color.Black);
            Chart.Plot.XAxis.MajorGrid(color: ColorTranslator.FromHtml("#191e23"));

            Chart.Plot.YAxis.Grid(false);
            Chart.Plot.YAxis2.Grid(true);
            Chart.Plot.YAxis2.TickLabelStyle(color: Color.Black);
            Chart.Plot.YAxis2.TickMarkColor(Color.Black);
            Chart.Plot.YAxis2.MajorGrid(color: ColorTranslator.FromHtml("#191e23"));

            candlePlot.ColorDown = ColorTranslator.FromHtml("#ff005b");
            candlePlot.ColorUp = ColorTranslator.FromHtml("#00d387");

            Chart.Refresh();
            Chart.Plot.SaveFig("finance_dateTimeAxis.png");
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