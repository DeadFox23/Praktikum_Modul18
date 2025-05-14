using System;
using System.IO;
using ConsoleApp1;
using System.Threading.Tasks;
using MySqlConnector;
using CsvHelper;
using System.Globalization;
using System.Diagnostics;

class Program
{
	public static async Task Main(string[] args)
	{
		//DataImport();
		//await DatabaseService.InsertCustomersAsync("Tassilo", "Weitzel", "janosgroettner@example.org", "2da9a179462a2f0cacb10b75423669d3e0b81117e3ea7e6b7c8dd16e462e250f", "Apolda", "14152", "Kargeplatz", "23");
		//await DatabaseService.InsertOrderAsync("Lorenzo", "Austermühle", "pmohaupt@example.com", "6e3f9362ca43fc50586b57fe0e7bfb6a899c617e66dfb8aa99919c0eeaebbb13", "Wolfach", "70436", "Seifertstr.", "41", "Mauspad", "12.5", "5", "dreck", "zubehör", "1");	
		await CsvImporter.ImportCsv("C:\\datenbanken\\e.txt");
	}
}
