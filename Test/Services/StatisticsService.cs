using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace FailSafeWeatherMonitor.Services
{
    public class StatisticsService
    {
        public void ShowStatistics()
        {
            string station = SelectStation();

            ShowStationStatistics(station);

        }

        private string SelectStation()
        {
            using var db = new WeatherDbContext();

            var stations = db.WeatherDataPoints
                .Select(w => w.StationName)
                .Distinct()
                .ToList();

            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Välj station")
                    .AddChoices(stations));

        }

        private void ShowStationStatistics(string stationName)
        {
            using var db = new WeatherDbContext();

            var measurements = db.WeatherDataPoints
                .Where(w => w.StationName == stationName)
                .ToList();

            int count = measurements.Count;

            double maxTemperature =
                measurements.Max(w => w.Temperature);

            double minTemperature =
                measurements.Min(w => w.Temperature);

            double averageTemperature =
                measurements.Average(w => w.Temperature);

            double averageHumidity =
                measurements.Average(w => w.Humidity);

            var table = new Table();

            table.Title =
                new TableTitle(
                    $"[green bold]Statistik för {stationName}[/]");

            table.Border(TableBorder.Rounded);

            table.AddColumn("[purple bold]Station[/]");
            table.AddColumn("[purple bold]Antal[/]");
            table.AddColumn("[purple bold]Max Temp[/]");
            table.AddColumn("[purple bold]Min Temp[/]");
            table.AddColumn("[purple bold]Medel Temp[/]");
            table.AddColumn("[purple bold]Medel Luftfuktighet[/]");

            table.AddRow(
                stationName,
                count.ToString(),
                $"{maxTemperature:F2}",
                $"{minTemperature:F2}",
                $"{averageTemperature:F2}",
                $"{averageHumidity:F2}");

            AnsiConsole.Write(table);

            var displayChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Vilka mätningar vill du se?")
                    .AddChoices(
                        "Senaste 15 mätningarna",
                        "Alla mätningar",
                        "Visa inte mätningar"));

            var measurementTable = new Table();

            measurementTable.Title =
                new TableTitle(
                    $"[green bold]Mätningar för {stationName}[/]");

            measurementTable.Border(TableBorder.Rounded);

            measurementTable.AddColumn("[green]Tid[/]");
            measurementTable.AddColumn("[green]Temperatur[/]");
            measurementTable.AddColumn("[green]Luftfuktighet[/]");

            IEnumerable<WeatherDataPoint> dataToShow;

            if (displayChoice == "Alla mätningar")
            {
                dataToShow = measurements
                    .OrderByDescending(m => m.Timestamp);
            }
            else if (displayChoice == "Senaste 15 mätningarna")
            {
                dataToShow = measurements
                    .OrderByDescending(m => m.Timestamp)
                    .Take(15);
            }
            else
            {
                return;
            }

            foreach (var measurement in dataToShow)
            {
                measurementTable.AddRow(
                    measurement.Timestamp.ToString("yyyy-MM-dd HH:mm"),
                    measurement.Temperature.ToString("F2"),
                    measurement.Humidity.ToString("F2"));
            }

            AnsiConsole.Write(measurementTable);
        }


    }
}
