using System;
using System.IO;

class Program
{
	public static void Main(string[] args)
	{
		DataImport();
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
