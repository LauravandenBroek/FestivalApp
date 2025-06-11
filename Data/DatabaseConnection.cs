using Microsoft.Data.SqlClient;



namespace Data
{
    public class DatabaseConnection
    {
        private readonly string _connectionString;

        public DatabaseConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected SqlConnection GetConnection()
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException("Connection string is not initialized.");
            }
            return new SqlConnection(_connectionString);
        }

    }
}
