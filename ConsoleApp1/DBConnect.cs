using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace ConsoleApp1
{
    public sealed class DBConnect
    {
        private MySqlConnection connection;
        private DBConnect(){ }
        public static DBConnect Instance =>Nested.Instance;

        private class Nested
        {
            static Nested() { }
            internal static readonly DBConnect Instance = new DBConnect();
        }
        public async Task InitializeAsync()
        {
            if(connection != null && connection.State == System.Data.ConnectionState.Open)
            {
                return;
            }
			string connectionString = "Server=localhost;User ID=root;Password=;Database=modul18_leicht;";
			connection = new MySqlConnection(connectionString);
			try
            {
                await connection.OpenAsync();
                //Console.WriteLine("MySql connection open");
            }
            catch (MySqlException e)
            {
                Console.WriteLine($"Error connecting to DB: {e.Message}");
                throw;
            }
        }
        public MySqlConnection GetConnection()
        {
            if (connection == null || connection.State != System.Data.ConnectionState.Open)
                throw new InvalidOperationException("no");
            return connection;
        }
        public void Close()
        {
            if(connection!=null&& connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
                //Console.WriteLine("SQL connection closed");
            }
        }

    }
}
