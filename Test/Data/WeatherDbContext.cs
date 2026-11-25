using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


public class WeatherDbContext: DbContext
{
    public DbSet<WeatherDataPoint> WeatherDataPoints { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		var configuration = new ConfigurationBuilder()								//Skapar en variabiler som ska konfigurera med jsonfilen
			.SetBasePath(AppContext.BaseDirectory)
			.AddJsonFile("appsettings.json")
			.Build();

        var connectionString = configuration["ConnectionStrings:DefaultConnection"];	//Skapar en variabel som innehåller värdet av jsonfilens Connectionsträng
        optionsBuilder.UseSqlServer(connectionString);								// konfigurerar med variablen skapad ovanför
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
