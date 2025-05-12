using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;


namespace ConsoleApp1
{
	public static class DatabaseService
	{
		public static async Task ConnectToDbAsync(string query)
		{
			await DBConnect.Instance.InitializeAsync();

			var conn = DBConnect.Instance.GetConnection();
			using var cmd = new MySqlCommand(query, conn);

			try
			{
				var result = await cmd.ExecuteScalarAsync();
				Console.WriteLine($"Querry result:{result}");
			}
			catch (Exception ex) { Console.WriteLine(ex.Message); }
			finally { DBConnect.Instance.Close(); }
		}
		public static async Task InsertCustomersAsync(string firstname,string lastname, string email,string password, string ort,string plz,string straße, string hausnummer)
		{
			await DBConnect.Instance.InitializeAsync();
			var conn = DBConnect.Instance.GetConnection();

			string query = " ";
			using var cmd = new MySqlCommand(query, conn);

			cmd.Parameters.AddWithValue("@firstname", firstname);
			cmd.Parameters.AddWithValue("@lastname", lastname);
			cmd.Parameters.AddWithValue("email", email);
			cmd.Parameters.AddWithValue("password", password);

			using var transaction = await conn.BeginTransactionAsync();

			try
			{
				string insertadress = @"INSERT INTO `wohnort` (`Ort`, `PLZ`, `Straße`, `Hausnummer`) VALUES ('@ort', '@plz', '@straße', '@hausnummer');
										Select Last_Insert_ID();";

				using var cmdAdress = new MySqlCommand(insertadress, conn, transaction);
				cmdAdress.Parameters.AddWithValue("@ort",ort);
				cmdAdress.Parameters.AddWithValue("@plz",plz);
				cmdAdress.Parameters.AddWithValue("@straße", straße);
				cmdAdress.Parameters.AddWithValue("@hausnummer", hausnummer);

				var oid = Convert.ToInt32(await cmdAdress.ExecuteScalarAsync());

				string insertCustomer = "@INSERT INTO `kunde` (`Vorname`, `Nachname`, `E-Mail`, `Passwort`, `OID`) VALUES ('@firstname', '@lastname', '@email', '@password', '@oid');";
				using var cmdCustomer = new MySqlCommand(insertCustomer, conn);
				cmdCustomer.Parameters.AddWithValue();
				cmdCustomer.Parameters.AddWithValue();
				cmdCustomer.Parameters.AddWithValue();
				cmdCustomer.Parameters.AddWithValue();
			}
		}
	}
}
