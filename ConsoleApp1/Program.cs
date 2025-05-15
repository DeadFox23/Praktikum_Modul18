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
		await CsvImporter.ImportCsv("C:\\datenbanken\\e.csv");
	}
}
