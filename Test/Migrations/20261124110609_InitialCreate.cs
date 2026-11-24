using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test.Migrations;

/// <inheritdoc />
public partial class _20261124110609_InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "WeatherDataPoints",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                StationName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                Temperature = table.Column<double>(type: "float", nullable: false),
                Humidity = table.Column<double>(type: "float", nullable: false),
                DataSource = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_WeatherDataPoints", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "WeatherDataPoints");
    }
}
