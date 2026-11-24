using System;
using Microsoft.EntityFrameworkCore;

public class WeatherDbContext: DbContext
{
	public DbSet<WeatherDataPoint> WeatherDataPoints { get; set; } = null!;

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer(
			"Server = (localdb)\\MSSQLLocalDB; Database = WeatherMonitorDb; Trusted_Connection = True;");
    }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<WeatherDataPoint>()
			.HasIndex(w => new
			{
				w.StationName,
				w.Timestamp
			})
		.IsUnique();
	}


}
