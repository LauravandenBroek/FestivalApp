using Interfaces.Models;
using Microsoft.Data.SqlClient;
using Interfaces;
using Logic.Exceptions;
using Microsoft.Extensions.Logging;

namespace Data
{
    public class RaveWishlistRepository : DatabaseConnection, IRaveWishlistRepository
    {

        private readonly ILogger<RaveWishlistRepository> _logger;
        public RaveWishlistRepository(ILogger<RaveWishlistRepository> logger, string connectionString) : base(connectionString)
        {
            _logger = logger;
        }


        public void AddRaveToWishlist(int UserId, int RaveId)
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
                    string sql = @"INSERT INTO UserWishlist (User_ID, Rave_ID) VALUES (@User_ID, @Rave_ID)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@User_ID", UserId);
                        command.Parameters.AddWithValue("@Rave_ID", RaveId);

                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    _logger.LogError(ex, "Failed to insert rave into wishlist");
                    throw new PersistentDatabaseException();
                }
            finally
            {
                connection?.Close();
            }
        }

        public List<Rave> GetRaveWishlistByUserId(int userId, int limit = 0)
        {
            var raves = new List<Rave>();
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
                           INNER JOIN UserWishlist UR ON R.ID = UR.Rave_ID
                           WHERE UR.User_ID = @UserId";

                if (limit > 0)
                {
                    sql += " ORDER BY R.Date ASC OFFSET 0 ROWS FETCH NEXT @Limit ROWS ONLY";
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
                _logger.LogError(ex, "Failed to retrieve wishlist from the database.");
                throw new PersistentDatabaseException();
            }
            
            finally
            {
                connection?.Close();
            }

            return raves;
        }



        public void RemoveRaveFromWishlist(int userId, int raveId)
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
                    string sql = @"DELETE FROM UserWishlist
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
                    _logger.LogError(ex, "Failed to remove rave from wishlist.");
                    throw new PersistentDatabaseException();
                }
            
            finally
            {
                connection?.Close();
            }
        }

    }
}
