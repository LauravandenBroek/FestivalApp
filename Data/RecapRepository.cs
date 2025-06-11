using Interfaces;
using Interfaces.Models;
using Logic.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace Data
{
    public class RecapRepository : DatabaseConnection, IRecapRepository
    {
        private readonly ILogger<RecapRepository> _logger;
        public RecapRepository(ILogger<RecapRepository> logger, string connectionString) : base(connectionString)
        {
            _logger = logger;
        }
        public void AddRecap(Recap recap, int userId)
        {
            SqlConnection connection = null;

            try
            {
                connection = GetConnection();

                try
                {
                    connection.Open();
                }
                catch (SqlException ex)
                {
                    _logger.LogError(ex, "No database connection");
                    throw new TemporaryDatabaseException();
                }

                try
                {
                    string recapSql = @"INSERT INTO UserRecap (User_ID, Rave, Description) 
                                VALUES (@User_ID, @Rave, @Description); SELECT SCOPE_IDENTITY();";
                    int recapId;

                    using (SqlCommand command = new SqlCommand(recapSql, connection))
                    {
                        command.Parameters.AddWithValue("@User_ID", userId);
                        command.Parameters.AddWithValue("@Rave", recap.Rave);
                        command.Parameters.AddWithValue("@Description", recap.Description);

                        recapId = Convert.ToInt32(command.ExecuteScalar());
                    }

                    string photoSql = @"INSERT INTO Album (UserRecapID, Photo) VALUES (@UserRecapID, @Photo)";

                    foreach (var photo in recap.Album)
                    {
                        using (SqlCommand photoCommand = new SqlCommand(photoSql, connection))
                        {
                            photoCommand.Parameters.AddWithValue("@UserRecapID", recapId);
                            photoCommand.Parameters.AddWithValue("@Photo", photo);
                            photoCommand.ExecuteNonQuery();
                        }
                    }
                }
                catch (SqlException ex)
                {
                    _logger.LogError(ex, "Failed to insert recap and album data into the database.");
                    throw new PersistentDatabaseException();
                }
            }
            finally
            {
                connection?.Close();
            }
        }


        public void DeleteRecap(int id)
        {
            throw new NotImplementedException();
        }

        public Recap ? GetRecapById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Recap> GetRecapsByUserId(int UserId)
        {
            throw new NotImplementedException();
        }

        public void UpdateRecap(Recap recap)
        {
            throw new NotImplementedException();
        }
    }
}
