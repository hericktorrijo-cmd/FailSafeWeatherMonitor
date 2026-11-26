# FailSafeWeatherMonitor

Vi har en konsol applikation som hanterar väder data från både CSV-fil och Open-Meteo API, all data lagras i SQL Server-databas med Entity Framework Core.

Conncetionstring finns i appsettings.json enligt uppgiftens instruktioner. 

CSV: Vi kan läsa in CSV-filen vi fick inför uppgiften och vi kastar bort korrupta rader, loggar felaktiga rader till en fil bad_data.log, vi förhindrar dubbletter och validerar data med TryParse.

API: Vi hämtar live-data från Open-Meteo API och sparar i databasen, vi har även en try-catch som var en av kraven. Även här förhindrar vi dubbletter.

Statistik: Vi har en lista över alla mätstationer, vi kan se statistik per station där vi har medel luftfuktighet, medeltemperatur, lägsta temperatur och högsta temperatur. Vi har även alternativet att
läsa de senaste 15 mätningarna eller samtliga mätningar eller till och med ingenting. 

Herick Torrijo
