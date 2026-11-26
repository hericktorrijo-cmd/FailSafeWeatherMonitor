using System;
using System.Collections.Generic;
using System.Text;

namespace FailSafeWeatherMonitor.Models
{
    public class OpenMeteoResponse
    {
        public Hourly Hourly { get; set; } = null!;
    }
}
