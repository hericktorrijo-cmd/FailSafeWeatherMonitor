using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Test.Models;

namespace Test.Services
{
    public class WeatherApiService
    {
        public void GetWeather()
        {
            try {

                string url = "https://api.open-meteo.com/v1/forecast?latitude=57.6832&longitude=11.9511&hourly=temperature_2m,relative_humidity_2m";

                var client = new HttpClient();

                var json = client.GetStringAsync(url).Result;

                var response = JsonSerializer.Deserialize<OpenMeteoResponse>(json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                if (response == null)
                {
                    Console.WriteLine("Kunde inte läsa API-svaret.");
                    return;
                }

                using var db = new WeatherDbContext();

                int importedCount = 0;
                int duplicateCount = 0;

                for (int i = 0; i < response.Hourly.Time.Count; i++)
                {
                    var weather = new WeatherDataPoint
                    {
                        StationName = "OpenMeteo",

                        Timestamp =
                            DateTime.Parse(response.Hourly.Time[i]),

                        Temperature =
                            response.Hourly.Temperature_2m[i],

                        Humidity =
                            response.Hourly.Relative_Humidity_2m[i],

                        DataSource = "API"
                    };

                    bool exists = db.WeatherDataPoints.Any(w =>
                        w.StationName == weather.StationName &&
                        w.Timestamp == weather.Timestamp);

                    if (!exists)
                    {
                        db.WeatherDataPoints.Add(weather);
                        importedCount++;
                    }
                    else
                    {
                        duplicateCount++;
                    }
                }

                db.SaveChanges();

                Console.WriteLine(
                    $"{importedCount} API-mätningar importerades.");

                Console.WriteLine(
                    $"{duplicateCount} dubbletter hoppades över.");

            }

            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Fel vid API-anrop: {ex.Message}");
            }


        }
    }
}
