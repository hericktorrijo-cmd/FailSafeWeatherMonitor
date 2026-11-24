using System;

public class WeatherDataPoint
{
    public int Id { get; set; }
    public string StationName { get; set; } = null!;
    public DateTime Timestamp { get; set; }
    public double Temperature { get; set; }
    public double Humidity { get; set; }
    public string DataSource { get; set; } = null!;
}
