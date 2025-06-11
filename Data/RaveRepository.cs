using Interfaces.Models;
using Interfaces;
using Microsoft.Data.SqlClient;
using Logic.Exceptions;
using Microsoft.Extensions.Logging;

namespace Data
{
    public class RaveRepository : DatabaseConnection, IRaveRepository
    {

        private readonly ILogger<RaveRepository> _logger;
        public RaveRepository(ILogger<RaveRepository> logger, string connectionString) : base(connectionString)
        {
            _logger = logger;
        }

        public void AddRave(Rave rave)
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
                string sql = @"INSERT INTO Rave (Name, Location, Date, Website, Min_age, Ticket_link, Description, Time, Image) 
                       VALUES (@Name, @Location, @Date, @Website, @Min_age, @Ticket_link, @Description, @Time, @Image)";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Name", rave.Name);
                    command.Parameters.AddWithValue("@Location", rave.Location);
                    command.Parameters.AddWithValue("@Date", rave.Date);
                    command.Parameters.AddWithValue("@Website", rave.Website);
                    command.Parameters.AddWithValue("@Min_age", rave.Minimum_age);
                    command.Parameters.AddWithValue("@Ticket_link", rave.Ticket_link);
                    command.Parameters.AddWithValue("@Description", rave.Description);
                    command.Parameters.AddWithValue("@Time", rave.Time);
                    command.Parameters.AddWithValue("@Image", rave.Image);

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Failed to insert rave data into the database.");
                throw new PersistentDatabaseException();
            }
            finally
            {
                connection?.Close();
            }
        }

        public List<Rave> GetRaves()
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
                List<Rave> raves = new List<Rave>();

                string sql = @"SELECT ID, Name, Location, Date, Website, Min_age, Ticket_link, Description, Time, Image FROM Rave";

                using (SqlCommand command = new SqlCommand(sql, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        byte[] image = reader.IsDBNull(reader.GetOrdinal("Image"))
                            ? null
                            : (byte[])reader["Image"];

                        raves.Add(new Rave(
                            reader.GetInt32(0), // ID
                            reader.GetString(1), // Name
                            reader.GetString(2), // Location
                            DateOnly.FromDateTime(reader.GetDateTime(3)), // Date
                            reader.GetString(4), // Website
                            reader.GetInt32(5), // Min_age
                            reader.GetString(6), // Ticket_link
                            reader.GetString(7), // Description
                            reader.GetString(8), // Time
                            image
                        ));
                    }
                }

                return raves;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Failed to retrieve raves from database.");
                throw new PersistentDatabaseException();
            }
            finally
            {
                connection?.Close();
            }
        }



        public Rave? GetRaveById(int id)
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
                string sql = @"SELECT Id, Name, Location, Date, Website, Min_age, Ticket_Link, Description, Time, Image 
                       FROM Rave WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Rave(
                                reader.GetInt32(0), // ID
                                reader.GetString(1), // Name
                                reader.GetString(2), // Location
                                DateOnly.FromDateTime(reader.GetDateTime(3)), // Date
                                reader.GetString(4), // Website
                                reader.GetInt32(5), // Min_age
                                reader.GetString(6), // Ticket_link
                                reader.GetString(7), // Description
                                reader.GetString(8), // Time
                                reader["Image"] as byte[] // Image
                            );
                        }
                    }
                }

                return null;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, $"Failed to retrieve rave with id {id}");
                throw new PersistentDatabaseException();
            }
            finally
            {
                connection?.Close();
            }
        }

        public void UpdateRave(Rave rave)
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
                string sql = @"UPDATE Rave 
                       SET Name = @Name, Location = @Location, Date = @Date, Website = @Website, 
                           Min_age = @Min_age, Ticket_link = @Ticket_link, Description = @Description, 
                           Time = @Time, Image = @Image 
                       WHERE Id = @ID";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ID", rave.Id);
                    command.Parameters.AddWithValue("@Name", rave.Name);
                    command.Parameters.AddWithValue("@Location", rave.Location);
                    command.Parameters.AddWithValue("@Date", rave.Date.ToDateTime(TimeOnly.MinValue));
                    command.Parameters.AddWithValue("@Website", rave.Website);
                    command.Parameters.AddWithValue("@Min_age", rave.Minimum_age);
                    command.Parameters.AddWithValue("@Ticket_link", rave.Ticket_link);
                    command.Parameters.AddWithValue("@Description", rave.Description);
                    command.Parameters.AddWithValue("@Time", rave.Time);
                    command.Parameters.AddWithValue("@Image", (object)rave.Image ?? DBNull.Value);

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, $"Failed to update rave with id {rave.Id}");
                throw new PersistentDatabaseException();
            }
            finally
            {
                connection?.Close();
            }
        }


        public void DeleteRave(int id)
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
                string sql = "DELETE FROM Rave WHERE id = @ID";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, $"Failed to delete rave with id {id}");
                throw new PersistentDatabaseException();
            }
            finally
            {
                connection?.Close();
            }
        }


        public List<Rave> GetUpcomingRaves(int count)
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
                List<Rave> raves = new List<Rave>();

                string sql = @"SELECT TOP (@Count) ID, Name, Location, Date, Website, Min_age, Ticket_link, Description, Time, Image 
                       FROM Rave 
                       WHERE Date >= GETDATE()
                       ORDER BY Date ASC";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Count", count);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            byte[] image = reader.IsDBNull(reader.GetOrdinal("Image"))
                                ? null
                                : (byte[])reader["Image"];

                            raves.Add(new Rave(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                DateOnly.FromDateTime(reader.GetDateTime(3)),
                                reader.GetString(4),
                                reader.GetInt32(5),
                                reader.GetString(6),
                                reader.GetString(7),
                                reader.GetString(8),
                                image
                            ));
                        }
                    }
                }

                return raves;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Failed to retrieve upcoming raves.");
                throw new PersistentDatabaseException();
            }
            finally
            {
                connection?.Close();
            }
        }



        public List<Rave> GetRavesPaged(int page, int pageSize)
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
                List<Rave> raves = new List<Rave>();

                string sql = @"
            SELECT ID, Name, Location, Date, Website, Min_age, Ticket_link, Description, Time, Image
            FROM Rave
            ORDER BY Date
            OFFSET @Offset ROWS
            FETCH NEXT @PageSize ROWS ONLY";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Offset", (page - 1) * pageSize);
                    command.Parameters.AddWithValue("@PageSize", pageSize);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            byte[] image = reader.IsDBNull(reader.GetOrdinal("Image"))
                                ? null
                                : (byte[])reader["Image"];

                            raves.Add(new Rave(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                DateOnly.FromDateTime(reader.GetDateTime(3)),
                                reader.GetString(4),
                                reader.GetInt32(5),
                                reader.GetString(6),
                                reader.GetString(7),
                                reader.GetString(8),
                                image
                            ));
                        }
                    }
                }

                return raves;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Failed to retrieve paged raves.");
                throw new PersistentDatabaseException();
            }
            finally
            {
                connection?.Close();
            }
        }

    }
}
