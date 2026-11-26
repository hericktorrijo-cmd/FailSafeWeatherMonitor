using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Models
{
    public class OpenMeteoResponse
    {
        public Hourly Hourly { get; set; } = null!;
    }
}
