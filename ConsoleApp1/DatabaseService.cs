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

			using var transaction = await conn.BeginTransactionAsync();

			try
			{
				//Adress
				var cmdAdress = conn.CreateCommand();
				cmdAdress.Transaction = transaction;
				cmdAdress.CommandText = @"INSERT INTO `wohnort` (`Ort`, `PLZ`, `Straße`, `Hausnummer`) VALUES ('@ort', '@plz', '@straße', '@hausnummer');";

				cmdAdress.Parameters.AddWithValue("@ort",ort);
				cmdAdress.Parameters.AddWithValue("@plz",plz);
				cmdAdress.Parameters.AddWithValue("@straße", straße);
				cmdAdress.Parameters.AddWithValue("@hausnummer", hausnummer);
				await cmdAdress.ExecuteNonQueryAsync();
				
				//ID
				var getIdCmd = conn.CreateCommand();
				getIdCmd.Transaction = transaction;
				getIdCmd.CommandText = "Select Last_Insert_ID();";
				var oid = Convert.ToInt32(await getIdCmd.ExecuteScalarAsync());
				
				//Customer
				var cmdCustomer = conn.CreateCommand();
				cmdCustomer.Transaction = transaction;
				cmdCustomer.CommandText = @"INSERT INTO `kunde` (`Vorname`, `Nachname`, `E-Mail`, `Passwort`, `OID`) VALUES ('@firstname', '@lastname', '@email', '@password', '@oid');";
				cmdCustomer.Parameters.AddWithValue("@firstname", firstname);
				cmdCustomer.Parameters.AddWithValue("@lastname", lastname);
				cmdCustomer.Parameters.AddWithValue("@email", email);
				cmdCustomer.Parameters.AddWithValue("@password", password);
				cmdCustomer.Parameters.AddWithValue("@oid",oid);
				await cmdCustomer.ExecuteNonQueryAsync();

				//Commit transaction
				await transaction.CommitAsync();
				Console.WriteLine("Customer added");
			}
			catch (Exception ex) 
			{
				await transaction.RollbackAsync();
				Console.WriteLine($"Error inserting customer: {ex.Message}"); 
			}
			finally { DBConnect.Instance.Close(); }
		}
	}
}
