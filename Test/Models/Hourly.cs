using System;
using System.Collections.Generic;
using System.Text;

namespace FailSafeWeatherMonitor.Models
{
    public class Hourly
    {
        public List<string> Time { get; set; } = new();
        public List<double> Temperature_2m { get; set; } = new();
        public List<double> Relative_Humidity_2m { get; set; } = new();

    }
}
