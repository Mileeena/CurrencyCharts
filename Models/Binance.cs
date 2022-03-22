using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ScottPlot;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using LiveChartsCore.Defaults;

namespace CurrencyCharts.Models
{
    ///https://api3.binance.com/api/v3/aggTrades?symbol=USDTRUB

    public class Binance
    {
        public List<FinancialPoint> pricesList { get; set; }
        public TimeSpan unitWidth { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbol">Имя пары на Binance (ETHUSDT)</param>
        /// <param name="interval">Вреенной промежуток (5m)</param>
        public Binance(string symbol, string interval)
        {
            pricesList = new List<FinancialPoint>();
            string url = $"https://api.binance.com/api/v1/klines?symbol={symbol}&interval={interval}";
            var request = new GetRequest(url);
            request.Run();

            var responce = request.Response;

            var source = responce;

            var dataList = JsonConvert.DeserializeObject<List<List<Datum>>>(source, Converter.Settings);
            foreach (var e in dataList)
            {
                double open = e[1].Double;
                double high = e[2].Double;
                double low = e[3].Double;
                double close = e[4].Double;
                DateTime timeStart = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(Convert.ToInt64(e[0].Double.ToString().Remove(10)));

                long tempTime = Convert.ToInt64(e[6].Double.ToString()) - Convert.ToInt64(e[0].Double.ToString());
                double volue = e[8].Double;

                unitWidth = TimeSpan.FromMilliseconds(tempTime);
                //DateTime date, double high, double open, double close, double low
                pricesList.Add(new FinancialPoint(timeStart, high, open, close, low));
            }
        }

        //[0]  1499040000000,      // Время открытия 
        //[1]  "0.01634790",       // Цена открытия (Open)
        //[2]  "0.80000000",       // Максимальная цена (High)
        //[3]  "0.01575800",       // Минимальная цена (Low)
        //[4]  "0.01577100",       // Цена закрытия (Close)
        //[5]  "148976.11427815",  // Объем
        //[6]  1499644799999,      // Время закрытия
        //[7]  "2434.19055334",    // Объем квотируемой валюты
        //[8]  308,                // Кол-во сделок
        //[9]  "1756.87402397",    // Taker buy base asset volume
        //[10] "28.46694368",      // Taker buy quote asset volume
        //[11] "17928899.62484339" // Ignore
        public  struct Datum
        {
            public long? Integer; 
            public double Double;

            public static implicit operator Datum(long Integer) => new Datum {Integer = Integer};
            public static implicit operator Datum(double Double) => new Datum { Double = Double};
        }

        internal static class Converter
        {
            public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
            {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                DateParseHandling = DateParseHandling.None,
                Converters =
                {
                    DatumConverter.Singleton,
                    new IsoDateTimeConverter {DateTimeStyles = DateTimeStyles.AssumeUniversal}
                },
            };
        }

        internal class DatumConverter : JsonConverter
        {
            public override bool CanConvert(Type x) => x == typeof(Datum) || x == typeof(Datum?);

            public override object ReadJson(JsonReader reader, Type x, object existingValue, JsonSerializer serializer)
            {
                switch (reader.TokenType)
                {
                    case JsonToken.Integer:
                        var doubleValue1 = serializer.Deserialize<double>(reader);
                        return new Datum { Double = doubleValue1 };
                    case JsonToken.String:
                    case JsonToken.Float:
                        var doubleValue = serializer.Deserialize<double>(reader);
                        return new Datum {Double = doubleValue };

                }

                throw new Exception("Cannot unmarshal type Datum");
            }

            public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
            {
                var value = (Datum) untypedValue;
                if (value.Integer != null)
                {
                    serializer.Serialize(writer, value.Integer.Value);
                    return;
                }

                if (value.Double != null)
                {
                    serializer.Serialize(writer, value.Double);
                    return;
                }

                throw new Exception("Cannot marshal type Datum");
            }

            public static readonly DatumConverter Singleton = new DatumConverter();
        }
    }
}
