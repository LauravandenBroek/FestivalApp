using Interfaces.Models;
using Microsoft.Data.SqlClient;
using Interfaces;
using Microsoft.Extensions.Logging;
using Logic.Exceptions;

namespace Data
{
    public class LineUpRepository : DatabaseConnection, ILineUpRepository
    {

        private readonly ILogger<LineUpRepository> _logger;
        public LineUpRepository(ILogger<LineUpRepository> logger, string connectionString) : base(connectionString)
        {
            _logger = logger;
        }

        public void AddLineUp(LineUp lineUp)
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
                string sql = @"INSERT INTO Line_up (Rave_ID, Artist_ID, Start_time, End_time, Stage) 
                       VALUES (@Rave_ID, @Artist_ID, @Start_time, @End_time, @Stage)";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Rave_ID", lineUp.Rave.Id);
                    command.Parameters.AddWithValue("@Artist_ID", lineUp.Artist.Id);
                    command.Parameters.AddWithValue("@Start_time", lineUp.StartTime);
                    command.Parameters.AddWithValue("@End_time", lineUp.EndTime);
                    command.Parameters.AddWithValue("@Stage", lineUp.Stage);

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Failed to add lineup.");
                throw new PersistentDatabaseException();
            }
            finally
            {
                connection?.Close();
            }
        }

        public List<LineUp> GetLineUpByRaveId(int raveId)
        {
            List<LineUp> lineUps = new List<LineUp>();
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
                string sql = @"SELECT l.Id, l.Start_time, l.End_time, l.Stage, 
                              a.Id AS ArtistId, a.Name AS ArtistName,
                              r.Id AS RaveId, r.Name AS RaveName
                       FROM Line_up l
                       JOIN Artist a ON l.Artist_ID = a.Id
                       JOIN Rave r ON l.Rave_ID = r.Id
                       WHERE l.Rave_ID = @Rave_ID
                       ORDER BY l.Start_time ASC";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Rave_ID", raveId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LineUp lineUp = new LineUp
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                StartTime = Convert.ToDateTime(reader["Start_time"]),
                                EndTime = Convert.ToDateTime(reader["End_time"]),
                                Stage = reader["Stage"].ToString(),
                                Artist = new Artist
                                {
                                    Id = Convert.ToInt32(reader["ArtistId"]),
                                    Name = reader["ArtistName"].ToString()
                                },
                                Rave = new Rave
                                {
                                    Id = Convert.ToInt32(reader["RaveId"]),
                                    Name = reader["RaveName"].ToString()
                                }
                            };

                            lineUps.Add(lineUp);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Failed to retrieve line-up by rave id.");
                throw new PersistentDatabaseException();
            }
            finally
            {
                connection?.Close();
            }

            return lineUps;
        }

        public void DeleteLineUp(int id)
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
                string sql = "DELETE FROM Line_up WHERE id = @ID";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, $"Failed to delete Line up with id {id}");
                throw new PersistentDatabaseException();
            }
            finally
            {
                connection?.Close();
            }
        }

    }
}
