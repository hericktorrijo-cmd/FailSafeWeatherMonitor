using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Services
{
    public class StatisticsService
    {
        public void ShowStatistics()
        {
            using var db = new WeatherDbContext();

            int count = db.WeatherDataPoints.Count();

            double maxTemperature = db.WeatherDataPoints.Max(w => w.Temperature);

            double minTemperature = db.WeatherDataPoints.Min(w => w.Temperature);

            double averageTemperature = db.WeatherDataPoints.Average(w => w.Temperature);

            double averageHumidity = db.WeatherDataPoints.Average(w => w.Humidity);

            Console.WriteLine($"Antal mätningar: {count}");
            Console.WriteLine($"Högsta temperaturen: {maxTemperature:F2}");
            Console.WriteLine($"Lägsta temperaturen: {minTemperature:F2}");
            Console.WriteLine($"Medeltemperatur: {averageTemperature:F2}");
            Console.WriteLine($"Medelluftfuktighet: {averageHumidity:F2}");
            
        }
    }
}
