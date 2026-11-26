using System;
using System.Collections.Generic;
using System.Text;
using Spectre.Console;


namespace FailSafeWeatherMonitor.Services
{
    public class MenuService
    {
        public void ShowMenu()
        {
            while(true)
            {


                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[purple bold]Välj ett alternativ[/]")
                        .AddChoices(
                        "Importera CSV",
                        "Hämta API-data",
                        "Visa statistik",
                        "Avsluta"));
                
                switch (choice)
                {
                    case "Importera CSV":
                        var importService = new WeatherImportService();
                        importService.Import();
                        break;

                    case "Hämta API-data":
                        var weatherApiService = new WeatherApiService();
                        weatherApiService.GetWeather();
                        break;

                    case "Visa statistik":
                        var statisticsService = new StatisticsService();
                        statisticsService.ShowStatistics();
                        break;

                    case "Avsluta":
                        return;
                }

                Console.WriteLine();
                Console.WriteLine("Tryck valfri tangent för att fortsätta...");
                Console.ReadKey();

                Console.Clear();
            }

        }
    }
}
