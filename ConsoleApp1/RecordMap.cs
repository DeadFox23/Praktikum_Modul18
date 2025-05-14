using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;

namespace ConsoleApp1
{
	public sealed class RecordMap : ClassMap<CsvRecord>
	{
		public RecordMap()
		{
			Map(m => m.Vorname);
			Map(m => m.Nachname);
			Map(m => m.Email);
			Map(m => m.Password);
			Map(m => m.Ort);
			Map(m => m.Plz);
			Map(m => m.Straße);
			Map(m => m.Hausnummer);
			Map(m => m.ProName);
			Map(m => m.Price);
			Map(m => m.Amount);
			Map(m => m.Description);
			Map(m => m.ArtNummer);
			Map(m => m.OrderAmount);
		}
	}
}
