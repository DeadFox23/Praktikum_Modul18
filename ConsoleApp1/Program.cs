using System;
using System.IO;
using ConsoleApp1;
using System.Threading.Tasks;
using MySqlConnector;
using CsvHelper;
using System.Globalization;
using System.Diagnostics;
using System.Runtime.CompilerServices;

class Program
{
	public static async Task Main(string[] args)
	{
		
		//await CsvImporter.ImportCsv("C:\\datenbanken\\e.csv");
		//await ManualImport.ReadFileCustomer("C:\\datenbanken\\ecust.csv");
		//await ManualImport.ReadFileProduct("C:\\datenbanken\\eprod.csv");
		//await ManualImport.ReadFileOrder("C:\\datenbanken\\eorder.csv");
		Console.WriteLine("Inport as one File[1] or as Seperate Files[2]");
		string input = Console.ReadLine();

		if( input == "1")
		{
			await CsvImporter.ImportCsv("C:\\datenbanken\\e.csv");
		}
		if ( input == "2")
		{
			await ManualImport.ReadFileCustomer("C:\\datenbanken\\a\\kundenkartei.csv");
			await ManualImport.ReadFileProduct("C:\\datenbanken\\a\\produktliste.csv");
			await ManualImport.ReadFileOrder("C:\\datenbanken\\a\\bestellung.csv");
		}
		else
		{
			Console.WriteLine("wrong input");
		}
	}
}
