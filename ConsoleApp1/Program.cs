using System;
using System.IO;
using ConsoleApp1;
using System.Threading.Tasks;
using MySqlConnector;

class Program
{
	public static async Task Main(string[] args)
	{
		string bbb = "bitch";
		//DataImport();
		await DatabaseService.ConnectToDbAsync("INSERT INTO `bitch` (`bitch1`, `bitch2`) VALUES (NULL, ${bitch}), (NULL, 'aaaaaaaaaaaaaaa'); ");
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
