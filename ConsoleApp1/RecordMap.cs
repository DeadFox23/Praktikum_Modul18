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
			//Festlegen von dem Mapping
			Map(m => m.Vorname).Index(0);
			Map(m => m.Nachname).Index(1);
			Map(m => m.Email).Index(2);
			Map(m => m.Password).Index(3);
			Map(m => m.Ort).Index(4);
			Map(m => m.Plz).Index(5);
			Map(m => m.Straße).Index(6);
			Map(m => m.Hausnummer).Index(7);
			Map(m => m.ProName).Index(8);
			Map(m => m.Price).Index(9);
			Map(m => m.Amount).Index(10);
			Map(m => m.Description).Index(11);
			Map(m => m.ArtNummer).Index(12);
			Map(m => m.OrderAmount).Index(13);
		}
	}
}
