using System;
using System.ComponentModel.DataAnnotations;

public class WeatherDataPoint
{
    public int Id { get; set; }

    [MaxLength(50)]
    public string StationName { get; set; } = null!;
    public DateTime Timestamp { get; set; }
    public double Temperature { get; set; }
    public double Humidity { get; set; }

    [MaxLength(50)]
    public string DataSource { get; set; } = null!;
}
