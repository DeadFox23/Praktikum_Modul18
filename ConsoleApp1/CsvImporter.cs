using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace ConsoleApp1
{
	public class CsvImporter
	{
		public static async Task ImportCsv(string path)
		{
			using var reader = new StreamReader(path);
			using var csv = new CsvReader(reader,CultureInfo.InvariantCulture);

			csv.Read();
			csv.ReadHeader();
			var records = csv.GetRecords<CsvRecord>().ToList();

			foreach (var record in records)
			{
				Console.WriteLine("ahhhh");
				try
				{
					await DatabaseService.InsertOrderAsync(
						record.Vorname,
						record.Nachname,
						record.Email,
						record.Password,
						record.Ort,
						record.Plz,
						record.Straße,
						record.Hausnummer,
						record.ProName,
						record.Price,
						record.Amount,
						record.Description,
						record.ArtNummer,
						record.OrderAmount);
					Console.WriteLine("aaaaaaaaa");
				}
				catch (Exception e)
				{
					Console.WriteLine("ups");
				}
			
			}
		}
	}
}
