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
            SqlConnection connection = null;

            try
            {
                connection = GetConnection();
                connection.Open();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "No database connection");
                throw new TemporaryDatabaseException();
            }

            try
            {
                string sql = "DELETE FROM Recap WHERE id = @ID";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, $"Failed to delete recap with id {id}");
                throw new PersistentDatabaseException();
            }
            finally
            {
                connection?.Close();
            }
        }

   
        public Recap GetRecapById(int recapId)
        {
            SqlConnection connection = null;

            try
            {
                connection = GetConnection();
                connection.Open();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "No database connection");
                throw new TemporaryDatabaseException();
            }

            try
            {
                Recap recap = null;

                string sql = @"SELECT ID, Rave, Description 
                       FROM UserRecap 
                       WHERE ID = @RecapId";

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@RecapId", recapId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            recap = new Recap(
                            reader.GetInt32(reader.GetOrdinal("ID")),
                            reader.GetString(reader.GetOrdinal("Rave")),
                            reader.GetString(reader.GetOrdinal("Description")),
                            new List<byte[]>()
                            );
                        }
                    }
                }

                if (recap != null)
                {
                    string photoSql = "SELECT Photo FROM Album WHERE UserRecapID = @RecapId";
                    using (var photoCommand = new SqlCommand(photoSql, connection))
                    {
                        photoCommand.Parameters.AddWithValue("@RecapId", recap.Id);
                        using (var photoReader = photoCommand.ExecuteReader())
                        {
                            while (photoReader.Read())
                            {
                                recap.Album.Add((byte[])photoReader["Photo"]);
                            }
                        }
                    }
                }

                return recap;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Failed to get recap by ID.");
                throw new PersistentDatabaseException();
            }
            finally
            {
                connection?.Close();
            }
        }




        public List<Recap> GetRecapsByUserId(int userId, int limit = 0)
        {
            SqlConnection connection = null;

            try
            {
                connection = GetConnection();
                connection.Open();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "No database connection");
                throw new TemporaryDatabaseException();
            }

            try
            {
                var recaps = new List<Recap>();
                string sql = @"SELECT ID, User_ID, Rave, Description 
                       FROM UserRecap 
                       WHERE User_ID = @UserId";

                if (limit > 0)
                {
                    sql += " ORDER BY ID ASC OFFSET 0 ROWS FETCH NEXT " + limit + " ROWS ONLY";
                }

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var recapId = reader.GetInt32(reader.GetOrdinal("ID"));
                            var recap = new Recap(
                                reader.GetInt32(reader.GetOrdinal("ID")),
                                reader.GetString(reader.GetOrdinal("Rave")),
                                reader.GetString(reader.GetOrdinal("Description")),
                                new List<byte[]>() 
                            );

                            recaps.Add(recap);
                        }
                    }
                }

                
                foreach (var recap in recaps)
                {
                    string photoSql = "SELECT Photo FROM Album WHERE UserRecapID = @RecapId";
                    using (var photoCommand = new SqlCommand(photoSql, connection))
                    {
                        photoCommand.Parameters.AddWithValue("@RecapId", recap.Id);
                        using (var photoReader = photoCommand.ExecuteReader())
                        {
                            while (photoReader.Read())
                            {
                                recap.Album.Add((byte[])photoReader["Photo"]);
                            }
                        }
                    }
                }

                return recaps;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Failed to get recaps.");
                throw new PersistentDatabaseException();
            }
            finally
            {
                connection?.Close();
            }
        }


        public void UpdateRecap(Recap recap)
        {
          
            SqlConnection connection = null;

            try
            {
                connection = GetConnection();
                connection.Open();
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "No database connection");
                throw new TemporaryDatabaseException();
            }

            try
            {
               
                string updateRecapSql = @"UPDATE UserRecap 
                                  SET Rave = @Rave, Description = @Description 
                                  WHERE ID = @RecapId";

                using (SqlCommand command = new SqlCommand(updateRecapSql, connection))
                {
                    command.Parameters.AddWithValue("@Rave", recap.Rave);
                    command.Parameters.AddWithValue("@Description", recap.Description);
                    command.Parameters.AddWithValue("@RecapId", recap.Id);
                    command.ExecuteNonQuery();
                }

                
                if (recap.Album != null && recap.Album.Count > 0)
                {
                    
                    string deletePhotosSql = @"DELETE FROM Album WHERE UserRecapID = @RecapId";
                    using (SqlCommand deleteCommand = new SqlCommand(deletePhotosSql, connection))
                    {
                        deleteCommand.Parameters.AddWithValue("@RecapId", recap.Id);
                        deleteCommand.ExecuteNonQuery();
                    }

                    
                    string insertPhotoSql = @"INSERT INTO Album (UserRecapID, Photo) VALUES (@UserRecapID, @Photo)";
                    foreach (var photo in recap.Album)
                    {
                        using (SqlCommand insertCommand = new SqlCommand(insertPhotoSql, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@UserRecapID", recap.Id);
                            insertCommand.Parameters.AddWithValue("@Photo", photo);
                            insertCommand.ExecuteNonQuery();
                        }
                    }
                }

            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Failed to update recap and album data.");
                throw new PersistentDatabaseException();
            }
            finally
            {
                connection?.Close();
            }
        }
    }
}
