using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Services
{
    public class MenuService
    {
        public void ShowMenu()
        {
            while(true)
            {
                

                Console.WriteLine("1: Importera CSV");
                Console.WriteLine("2: Visa statistik");
                Console.WriteLine("3: Avsluta");

                var choice = Console.ReadLine();
                
                switch (choice)
                {
                    case "1":
                        var importService = new WeatherImportService();
                        importService.Import();
                        break;

                    case "2":
                        var statisticsService = new StatisticsService();
                        statisticsService.ShowStatistics();
                        break;

                    case "3":
                        return;

                    default:
                        Console.WriteLine("Ogiltigt val");
                        break;
                }

                Console.WriteLine();
                Console.WriteLine("Tryck valfri tangent för att fortsätta...");
                Console.ReadKey();

                Console.Clear();
            }

        }
    }
}
