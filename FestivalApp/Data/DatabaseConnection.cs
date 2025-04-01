using FestivalApp.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace FestivalApp.Data
{
    public class DatabaseConnection
    {
        private readonly string _connectionString;

        public DatabaseConnection(IConfiguration configuration)
        {
            // Haal de connection string op uit appsettings.json
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }



        public bool TestConnection()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    return connection.State == System.Data.ConnectionState.Open;
                }
            }
            catch (Exception ex)
            {
                // Eventueel loggen of tonen voor debugging
                Console.WriteLine("Connection failed: " + ex.Message);
                return false;
            }
        }

    }
}
