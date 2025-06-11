using Interfaces.Models;
using Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Logic.Exceptions;


namespace Data
{
    public class AttendingRaveRepository : DatabaseConnection, IAttendingRaveRepository
    {

        private readonly ILogger<AttendingRaveRepository> _logger;
        public AttendingRaveRepository(ILogger<AttendingRaveRepository> logger, string connectionString) : base(connectionString)
        {
            _logger = logger;
        }

        public void AddRaveToAttendingList(int userId, int raveId)
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
                string sql = @"INSERT INTO UserRave (User_ID, Rave_ID) 
                       VALUES (@User_ID, @Rave_ID)";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@User_ID", userId);
                    command.Parameters.AddWithValue("@Rave_ID", raveId);

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Failed to add rave to attending list.");
                throw new PersistentDatabaseException();
            }
            finally
            {
                connection?.Close();
            }
        }



        public List<Rave> GetAttendingRavesByUserId(int userId, int limit = 0)
        {
            List<Rave> raves = new List<Rave>();
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
                string sql = @"SELECT R.* 
                       FROM Rave R
                       INNER JOIN UserRave UR ON R.ID = UR.Rave_ID
                       WHERE UR.User_ID = @UserId";

                if (limit > 0)
                {
                    sql += " ORDER BY R.Date ASC OFFSET 0 ROWS FETCH NEXT @Limit ROWS ONLY";
                }

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
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

                            raves.Add(new Rave(
                                reader.GetInt32(reader.GetOrdinal("ID")),
                                reader.GetString(reader.GetOrdinal("Name")),
                                reader.GetString(reader.GetOrdinal("Location")),
                                DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("Date"))),
                                reader.GetString(reader.GetOrdinal("Website")),
                                reader.GetInt32(reader.GetOrdinal("Min_age")),
                                reader.GetString(reader.GetOrdinal("Ticket_link")),
                                reader.GetString(reader.GetOrdinal("Description")),
                                reader.GetString(reader.GetOrdinal("Time")),
                                image
                            ));
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Error while retrieving attending raves.");
                throw new PersistentDatabaseException();
            }
            finally
            {
                connection?.Close();
            }

            return raves;
        }



        public void RemoveRaveFromAttendingList(int userId, int raveId)
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
                string sql = @"DELETE FROM UserRave 
                       WHERE User_ID = @User_ID AND Rave_ID = @Rave_ID";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@User_ID", userId);
                    command.Parameters.AddWithValue("@Rave_ID", raveId);

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "Error while removing rave from attending list");
                throw new PersistentDatabaseException();
            }
            finally
            {
                connection?.Close();
            }
        }

    }
}
