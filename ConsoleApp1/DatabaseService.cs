﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml.Linq;
using MySqlConnector;


namespace ConsoleApp1
{
	public static class DatabaseService
	{
		public static async Task<int> InsertLivingPlaceAsync(string ort, string plz, string straße, string hausnummer)
		{
			await DBConnect.Instance.InitializeAsync();
			var conn = DBConnect.Instance.GetConnection();
			using var transaction = await conn.BeginTransactionAsync();
			try
			{
				//Adress
				var cmdAdress = conn.CreateCommand();
				cmdAdress.Transaction = transaction;
				cmdAdress.CommandText = @"INSERT INTO `wohnort` (`Ort`, `PLZ`, `Straße`, `Hausnummer`) VALUES (@ort, @plz, @straße, @hausnummer);";

				cmdAdress.Parameters.AddWithValue("@ort", ort);
				cmdAdress.Parameters.AddWithValue("@plz", plz);
				cmdAdress.Parameters.AddWithValue("@straße", straße);
				cmdAdress.Parameters.AddWithValue("@hausnummer", hausnummer);
				await cmdAdress.ExecuteNonQueryAsync();

				//ID
				var getIdCmd = conn.CreateCommand();
				getIdCmd.Transaction = transaction;
				getIdCmd.CommandText = "Select Last_Insert_ID();";
				var oid = Convert.ToInt32(await getIdCmd.ExecuteScalarAsync());

				await transaction.CommitAsync();
				return oid;
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				Console.WriteLine($"Error inserting customer: {ex.Message}");
				return -1;
			}
			finally { DBConnect.Instance.Close(); }
		}
		public static async Task<int> InsertCustomersAsync(string firstname,string lastname, string email,string password, string ort, string plz, string straße, string hausnummer)
		{
			
			int oid = await InsertLivingPlaceAsync(ort, plz, straße,hausnummer);
			if (oid <= 0)
			{
				Console.WriteLine("Error Inserting Living Place");
				return -1;
			}
			await DBConnect.Instance.InitializeAsync();
			var conn = DBConnect.Instance.GetConnection();

			using var transaction = await conn.BeginTransactionAsync();
			
			try
			{

				//Customer
				var cmdCustomer = conn.CreateCommand();
				cmdCustomer.Transaction = transaction;
				cmdCustomer.CommandText = @"INSERT INTO `kunde` (`Vorname`, `Nachname`, `E-Mail`, `Passwort`, `OID`) VALUES (@firstname, @lastname, @email, @password, @oid);";
				
				cmdCustomer.Parameters.AddWithValue("@firstname", firstname);
				cmdCustomer.Parameters.AddWithValue("@lastname", lastname);
				cmdCustomer.Parameters.AddWithValue("@email", email);
				cmdCustomer.Parameters.AddWithValue("@password", password);
				cmdCustomer.Parameters.AddWithValue("@oid",oid);
				await cmdCustomer.ExecuteNonQueryAsync();

				var getIdCmd = conn.CreateCommand();
				getIdCmd.Transaction = transaction;
				getIdCmd.CommandText = "Select Last_Insert_ID();";
				var kid = Convert.ToInt32(await getIdCmd.ExecuteScalarAsync());

				//Commit transaction
				await transaction.CommitAsync();
				return kid;
			}
			catch (Exception ex) 
			{
				await transaction.RollbackAsync();
				Console.WriteLine($"Error inserting customer: {ex.Message}");
				return -1;
			}
			finally { DBConnect.Instance.Close(); }
		}
		public static async Task<int> InsertProductAsync(string name, string price, string amount, string description, string artNummer)
		{
			await DBConnect.Instance.InitializeAsync();
			var conn = DBConnect.Instance.GetConnection();

			using var transaction = await conn.BeginTransactionAsync();
			try
			{
				//Product
				var cmdProduct = conn.CreateCommand();
				cmdProduct.Transaction = transaction;
				cmdProduct.CommandText = @"INSERT INTO `produkte` (`Name`, `Verkaufspreis`, `Bestand`, `Beschreibung`, `Artikelnummer`) VALUES (@name, @price, @amount, @description, @artNummer) ;";

				cmdProduct.Parameters.AddWithValue("@name", name);
				cmdProduct.Parameters.AddWithValue("@price", price);
				cmdProduct.Parameters.AddWithValue("@amount", amount);
				cmdProduct.Parameters.AddWithValue("@description", description);
				cmdProduct.Parameters.AddWithValue("@artNummer", artNummer);
				await cmdProduct.ExecuteNonQueryAsync();

				//ID
				var getIdCmd = conn.CreateCommand();
				getIdCmd.Transaction = transaction;
				getIdCmd.CommandText = "Select Last_Insert_ID();";
				var bradpid = Convert.ToInt32(await getIdCmd.ExecuteScalarAsync());

				await transaction.CommitAsync();
				return bradpid;
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				Console.WriteLine($"Error inserting customer: {ex.Message}");
				return -1;
			}
			finally { DBConnect.Instance.Close(); }
		}
		public static async Task InsertOrderAsync(string firstname, string lastname, string email, string password, string ort, string plz, string straße, string hausnummer, string proName, string price, string amount, string description, string artNummer, string orderAmount)
		{
			int pid = await InsertProductAsync(proName, price, amount, description, artNummer);
			if (pid <= 0)
			{
				Console.WriteLine("Error Inserting Product");
				return;
			}
			int kid = await InsertCustomersAsync(firstname, lastname, email, password, ort, plz, straße, hausnummer);
			if (kid <= 0)
			{
				Console.WriteLine("Error Inserting Customer");
				return;
			}
			await DBConnect.Instance.InitializeAsync();
			var conn = DBConnect.Instance.GetConnection();

			using var transaction = await conn.BeginTransactionAsync();

			try
			{

				//Order
				var cmdOrder = conn.CreateCommand();
				cmdOrder.Transaction = transaction;
				cmdOrder.CommandText = @"INSERT INTO `bestellungen` (`PID`, `KID`, `Menge`) VALUES (@pid, @kid, @orderAmount);";
				cmdOrder.Parameters.AddWithValue("@pid", pid);
				cmdOrder.Parameters.AddWithValue("@kid", kid);
				cmdOrder.Parameters.AddWithValue("@orderAmount", orderAmount);
				await cmdOrder.ExecuteNonQueryAsync();

				//Commit transaction
				await transaction.CommitAsync();
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				Console.WriteLine($"Error inserting customer: {ex.Message}");
			}
			finally { DBConnect.Instance.Close(); }
		}
		public static async Task InsertOrderAsyncShort(string kid, string atk, string orderAmount)
		{
			int pid = await GetPid(atk);
			if (pid <= 0)
			{
				Console.WriteLine("No Product Found");
				return;
			}

			await DBConnect.Instance.InitializeAsync();
			var conn = DBConnect.Instance.GetConnection();

			using var transaction = await conn.BeginTransactionAsync();
			

			try
			{
				var cmdOrder = conn.CreateCommand();
				cmdOrder.Transaction = transaction;
				cmdOrder.CommandText = @"INSERT INTO `bestellungen` (`PID`, `KID`, `Menge`) VALUES (@pid, @kid, @orderAmount);";
				cmdOrder.Parameters.AddWithValue("@pid", pid);
				cmdOrder.Parameters.AddWithValue("@kid", kid);
				cmdOrder.Parameters.AddWithValue("@orderAmount", orderAmount);
				await cmdOrder.ExecuteNonQueryAsync();

				//Commit transaction
				await transaction.CommitAsync();
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				Console.WriteLine($"Error inserting customer: {ex.Message}");
			}
			finally { DBConnect.Instance.Close(); }
		}
		private static async Task<int> GetPid(string atk)
		{
			await DBConnect.Instance.InitializeAsync();
			var conn = DBConnect.Instance.GetConnection();

			using var transaction = await conn.BeginTransactionAsync();
			try
			{
				var cmd = conn.CreateCommand();
				cmd.Transaction = transaction;
				cmd.CommandText = @"SELECT PID from produkte WHERE Artikelnummer = @atk;";
				cmd.Parameters.AddWithValue("@atk", atk);
				var pid = Convert.ToInt32(await cmd.ExecuteScalarAsync());

				await transaction.CommitAsync();
				//Console.WriteLine($"pid {pid}");
				return pid;
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				Console.WriteLine($"Error getting PID: {ex.Message}");
				Console.WriteLine("wrong");
				return -1;
			}
			finally { DBConnect.Instance.Close(); }
		}
	}
}
