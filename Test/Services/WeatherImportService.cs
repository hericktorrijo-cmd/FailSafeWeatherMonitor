using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Test.Services
{
    public class WeatherImportService
    {
        public void Import()
        {
            var path = Path.Combine(AppContext.BaseDirectory, @"..\..\..\weather_data.csv");

            var lines = File.ReadAllLines(path);

            File.WriteAllText("bad_data.log", "");          //Skapar/tömmer loggfilen inför ny import

            using var db = new WeatherDbContext();

            int imported = 0;
            int rejected = 0;

            Console.WriteLine(Path.GetFullPath("bad_data.log"));            //Vill bara se vart min fil skapas

            foreach (var line in lines.Skip(1))
            {
                var parts = line.Split(                                  //Splitar allt så att vi kan försöka parsa och skapa objekt senare
                    '\t',
                    StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != 4)                                  //Kollar att alla värden finns med
                {

                    rejected++; File.AppendAllText("bad_data.log", line + Environment.NewLine);

                    continue;
                }

                if (!DateTime.TryParse(parts[1], out DateTime timestamp))
                {
                    File.AppendAllText("bad_data.log", line + Environment.NewLine);

                    rejected++;
                    continue;
                }

                if (!double.TryParse(
                    parts[2],
                    NumberStyles.Any,                                     //Tolka det som ett vanligt tal
                    CultureInfo.InvariantCulture,                         //Gör så att den använder sig av internationell format, accepterar .
                    out double temperature))
                {
                    File.AppendAllText("bad_data.log", line + Environment.NewLine);

                    rejected++;
                    continue;
                }

                if (!double.TryParse(
                    parts[3],
                    NumberStyles.Any,                                     //Tolka det som ett vanligt tal
                    CultureInfo.InvariantCulture,                         //Gör så att den använder sig av internationell format, accepterar .
                    out double humidity))
                {
                    File.AppendAllText("bad_data.log", line + Environment.NewLine);

                    rejected++;
                    continue;
                }

                if (temperature < -50 || temperature > 50)              //Kollar om talet är vettigt
                {
                    File.AppendAllText("bad_data.log", line + Environment.NewLine);

                    rejected++;
                    continue;
                }

                if (humidity < 0 || humidity > 100)                     //Kollar om talet är vettigt
                {
                    File.AppendAllText("bad_data.log", line + Environment.NewLine);

                    rejected++;
                    continue;
                }

                var weather = new WeatherDataPoint                      //Skapar objekt av allt
                {
                    StationName = parts[0],
                    Timestamp = timestamp,
                    Temperature = temperature,
                    Humidity = humidity,
                    DataSource = "CSV-File"
                };

                bool exist = db.WeatherDataPoints.Any(w =>                                 // Kollar om det finns dubletter
                w.StationName == weather.StationName && w.Timestamp == weather.Timestamp); // På stationsnamn och tidsstämpel    

                if (exist)
                {
                    rejected++;                                                     //Om dubblett finns forsätter vi till nästa rad
                    continue;
                }


                db.WeatherDataPoints.Add(weather);                  //Lägger till objektet, skickar dock inte till databasen än

                imported++;

                Console.WriteLine(                                  //Skriver ut det som vi vill ha det
                    $"{weather.StationName} | " +
                    $"{weather.Timestamp} | " +
                    $"{weather.Temperature} | " +
                    $"{weather.Humidity}");
            }

            db.SaveChanges();                                   //Skickar allt som är godkänt till databasen

            Console.WriteLine($"{imported} rader importerades, {rejected} rader förkastades."); //Bara för att se antal förkastade rader

        }

    }
}
