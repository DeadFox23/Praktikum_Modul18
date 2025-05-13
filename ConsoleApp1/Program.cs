using System;
using System.IO;
using ConsoleApp1;
using System.Threading.Tasks;
using MySqlConnector;

class Program
{
	public static async Task Main(string[] args)
	{
		//DataImport();
		//await DatabaseService.ConnectToDbAsync("INSERT INTO `bitch` (`bitch1`, `bitch2`) VALUES (NULL, 'bitch'), (NULL, 'aaaaaaaaaaaaaaa'); ");
		await DatabaseService.InsertCustomersAsync("Tassilo", "Weitzel", "janosgroettner@example.org", "2da9a179462a2f0cacb10b75423669d3e0b81117e3ea7e6b7c8dd16e462e250f", "Apolda", "14152", "Kargeplatz", "23");
	}

	private static void DataImport()
	{
		Console.Write("C:\\datenbanken\\e.txt");
		string filePath = Console.ReadLine();

		if (File.Exists(filePath))
		{
			string fileContent = File.ReadAllText(filePath);
			Console.WriteLine("\nFile content:\n");
			Console.WriteLine(fileContent);
		}
		else
		{
			Console.WriteLine("File not found.");
		}

		Console.WriteLine("\nPress any key to exit...");
		Console.ReadKey();
	}
}
