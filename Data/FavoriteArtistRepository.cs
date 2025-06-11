using Interfaces.Models;
using Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Logic.Exceptions;

namespace Data
{
    public class FavoriteArtistRepository : DatabaseConnection, IFavoriteArtistRepository
    {
        private readonly ILogger<FavoriteArtistRepository> _logger;
        public FavoriteArtistRepository(ILogger<FavoriteArtistRepository> logger, string connectionString) : base(connectionString)
        {
            _logger = logger;
        }
        public void AddArtistToFavorites(int userId, int artistId)
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
                string sql = @"INSERT INTO UserArtist (User_ID, Artist_ID) 
                       VALUES (@User_ID, @Artist_ID)";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@User_ID", userId);
                    command.Parameters.AddWithValue("@Artist_ID", artistId);

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Failed to add artist to favorites.");
                throw new PersistentDatabaseException();
            }
            finally
            {
                connection?.Close();
            }
        }


        public List<Artist> GetFavoriteArtistsByUserId(int userId, int limit = 0)
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
                var artists = new List<Artist>();

                string sql = @"SELECT A.* 
                       FROM Artist A
                       INNER JOIN UserArtist UA ON A.ID = UA.Artist_ID
                       WHERE UA.User_ID = @UserId";

                if (limit > 0)
                {
                    sql += " ORDER BY A.Name ASC OFFSET 0 ROWS FETCH NEXT @Limit ROWS ONLY";
                }

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    if (limit > 0)
                    {
                        command.Parameters.AddWithValue("@Limit", limit);
                    }

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            byte[] image = reader.IsDBNull(reader.GetOrdinal("Image"))
                                ? null
                                : (byte[])reader["Image"];

                            artists.Add(new Artist(
                                reader.GetInt32(reader.GetOrdinal("ID")),
                                reader.GetString(reader.GetOrdinal("Name")),
                                reader.GetString(reader.GetOrdinal("Nationality")),
                                reader.GetString(reader.GetOrdinal("Genre")),
                                reader.GetString(reader.GetOrdinal("Description")),
                                image
                            ));
                        }
                    }
                }

                return artists;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Failed to get favorite artists.");
                throw new PersistentDatabaseException();
            }
            finally
            {
                connection?.Close();
            }
        }



        public void RemoveArtistFromFavorites(int userId, int artistId)
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
                string sql = @"DELETE FROM UserArtist 
                       WHERE User_ID = @User_ID AND Artist_ID = @Artist_ID";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@User_ID", userId);
                    command.Parameters.AddWithValue("@Artist_ID", artistId);

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Failed to remove artist from favorites.");
                throw new PersistentDatabaseException();
            }
            finally
            {
                connection?.Close();
            }
        }
    }
}

