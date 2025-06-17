using Interfaces.Models;
using Microsoft.Data.SqlClient;
using Interfaces;
using Microsoft.Extensions.Logging;
using Logic.Exceptions;


namespace Data
{
    public class ArtistRepository : DatabaseConnection, IArtistRepository
    {
        private readonly ILogger<ArtistRepository> _logger;
        public ArtistRepository(ILogger<ArtistRepository> logger, string connectionString)
            : base(connectionString)
        {
            _logger = logger;
        }


        public void AddArtist(Artist artist)
        {
            SqlConnection connection = null;

            try
            {
                connection = GetConnection();
                connection.Open();
            }
            catch (SqlException ex)
            {
                throw new TemporaryDatabaseException();
            }

            try
            {
                string sql = @"INSERT INTO Artist (Name, Nationality, Genre, Description, Image) 
                       VALUES (@Name, @Nationality, @Genre, @Description, @Image)";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Name", artist.Name);
                    command.Parameters.AddWithValue("@Nationality", artist.Nationality);
                    command.Parameters.AddWithValue("@Genre", artist.Genre);
                    command.Parameters.AddWithValue("@Description", artist.Description);
                    command.Parameters.AddWithValue("@Image", (object)artist.Image ?? DBNull.Value);

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw new PersistentDatabaseException();
            }
            finally
            {
                connection?.Close();
            }
        }


        public List<Artist> GetArtists(int limit = 0)
        {
            List<Artist> artists = new List<Artist>();
            SqlConnection connection = null;

            try
            {
                connection = GetConnection();
                connection.Open();
            }
            catch (SqlException)
            {
                throw new TemporaryDatabaseException();
            }

            try
            {
                string sql = "SELECT Id, Name, Nationality, Genre, Description, Image FROM Artist";


                if (limit > 0)
                {
                    sql += " ORDER BY ID ASC OFFSET 0 ROWS FETCH NEXT @Limit ROWS ONLY";
                }

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    if (limit > 0)
                    {
                        command.Parameters.AddWithValue("@Limit", limit);
                    }

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            byte[] image = reader.IsDBNull(reader.GetOrdinal("Image"))
                                ? null
                                : (byte[])reader["Image"];

                            artists.Add(new Artist(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                reader.GetString(4),
                                image
                            ));
                        }
                    }
                }
            }
            catch (SqlException)
            {
                throw new PersistentDatabaseException();
            }
            finally
            {
                connection?.Close();
            }

            return artists;
        }



        public Artist? GetArtistById(int id)
        {
            SqlConnection connection = null;

            try
            {
                connection = GetConnection();
                connection.Open();
            }
            catch (SqlException)
            {
                throw new TemporaryDatabaseException();
            }

            try
            {
                string sql = "SELECT Id, Name, Nationality, Genre, Description, Image FROM Artist WHERE id = @Id";

                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Id", id);

                using var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return new Artist(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(4),
                        reader.IsDBNull(reader.GetOrdinal("Image")) ? null : (byte[])reader["Image"]
                    );
                }
            }
            catch (SqlException)
            {
                throw new PersistentDatabaseException();
            }
            finally
            {
                connection?.Close();
            }

            return null;
        }



        public void UpdateArtist(Artist artist)
        {
            SqlConnection connection = null;

            try
            {
                connection = GetConnection();
                connection.Open();
            }
            catch (SqlException)
            {
                throw new TemporaryDatabaseException();
            }

            try
            {
                string sql = @"UPDATE Artist 
                       SET Name = @Name, Nationality = @Nationality, Genre = @Genre, Description = @Description, Image = @Image 
                       WHERE Id = @ID";

                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@ID", artist.Id);
                command.Parameters.AddWithValue("@Name", artist.Name);
                command.Parameters.AddWithValue("@Nationality", artist.Nationality);
                command.Parameters.AddWithValue("@Genre", artist.Genre);
                command.Parameters.AddWithValue("@Description", artist.Description);
                command.Parameters.AddWithValue("@Image", (object)artist.Image ?? DBNull.Value);

                command.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                throw new PersistentDatabaseException();
            }
            finally
            {
                connection?.Close();
            }
        }

        public void DeleteArtist(int id)
        {
            SqlConnection connection = null;

            try
            {
                connection = GetConnection();
                connection.Open();
            }
            catch (SqlException)
            {
                throw new TemporaryDatabaseException();
            }

            try
            {
                string sql = "DELETE FROM Artist WHERE Id = @Id";

                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                throw new PersistentDatabaseException();
            }
            finally
            {
                connection?.Close();
            }
        }


        public List<Artist> GetArtistsPaged(int page, int pageSize)
        {
            SqlConnection connection = null;
            List<Artist> artists = new List<Artist>();

            try
            {
                connection = GetConnection();
                connection.Open();
            }
            catch (SqlException)
            {
                throw new TemporaryDatabaseException();
            }

            try
            {
                string sql = "SELECT Id, Name, Nationality, Genre, Description, Image FROM Artist ORDER BY Id OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Offset", (page - 1) * pageSize);
                command.Parameters.AddWithValue("@PageSize", pageSize);

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    byte[] image = reader.IsDBNull(reader.GetOrdinal("Image")) ? null : (byte[])reader["Image"];

                    artists.Add(new Artist(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(4),
                        image
                    ));
                }
            }
            catch (SqlException)
            {
                throw new PersistentDatabaseException();
            }
            finally
            {
                connection?.Close();
            }

            return artists;
        }

    }

}
