using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
	public class ManualImport
	{
		public static async Task ReadFileCustomer(string path)
		{
			string header = "Kundennummer,Vorname,Nachname,PLZ,Stadt,Straße,Hausnr,E-Mail,Passwort";
			

			if (!File.Exists(path))
			{
				Console.WriteLine("no file found");
				return;
			}
			using (var readhead = new StreamReader(path))
			{
				string firstline = readhead.ReadLine();
				if(firstline == null || firstline.Trim() != header)
				{
					Console.WriteLine("wrong file");
					return;
				}
			}

			var lines = File.ReadLines(path).Skip(1);
			foreach (var line in lines)
			{
				try
				{
					string[] words = line.Split(',');
					await DatabaseService.InsertCustomersAsync(words[1], words[2], words[6], words[5], words[7], words[8], words[3], words[4]);
					Console.WriteLine("Customer added");
				}
				catch { Console.WriteLine("uuuuuuuuu"); }

			}
		}

		public static async Task ReadFileProduct(string path)
		{
			string header = "Produktname,Verkaufspreis,Bestand,Beschreibung,Artikelnummer";

			if (!File.Exists(path))
			{
				Console.WriteLine("no file found");
				return;
			}
			using (var readhead = new StreamReader(path))
			{
				string firstline = readhead.ReadLine();
				if (firstline == null || firstline.Trim() != header)
				{
					Console.WriteLine("wrong file");
					return;
				}
			}
			var lines = File.ReadLines(path).Skip(1);
			foreach (var line in lines)
			{
				try
				{
					string[] words = line.Split(',');
					await DatabaseService.InsertProductAsync(words[0], words[1], words[2], words[3], words[4]);
					Console.WriteLine("Product added");
				}
				catch { Console.WriteLine("uuuuuuuuu"); }

			}
		}
		public static async Task ReadFileOrder(string path)
		{
			string header = "Kundennummer,Artikelnummer,Menge";

			if (!File.Exists(path))
			{
				Console.WriteLine("no file found");
				return;
			}
			using (var readhead = new StreamReader(path))
			{
				string firstline = readhead.ReadLine();
				if (firstline == null || firstline.Trim() != header)
				{
					Console.WriteLine("wrong file");
					return;
				}
			}
			var lines = File.ReadLines(path).Skip(1);
			foreach (var line in lines)
			{
				try
				{
					string[] words = line.Split(',');
					await DatabaseService.InsertOrderAsyncShort(words[0], words[1], words[2]);
					Console.WriteLine("Order added");
				}
				catch { Console.WriteLine("uuuuuuuuuhh"); }

			}
		}

	}
}
