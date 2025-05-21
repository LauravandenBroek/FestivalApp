using Interfaces.Models;
using Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class AttendingRaveRepository : DatabaseConnection, IAttendingRaveRepository
    {
        public AttendingRaveRepository(string connectionString) : base(connectionString)
        {
        }

        public void AddRaveToAttendingList(int UserId, int RaveId)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string sql = @"INSERT INTO UserRave (User_ID, Rave_ID) 
                           VALUES (@User_ID, @Rave_ID)";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@User_ID", UserId);
                    command.Parameters.AddWithValue("@Rave_ID", RaveId);


                    command.ExecuteNonQuery();
                }
            }
        }



        public List<Rave> GetAttendingRavesByUserId(int userId, int limit = 0)
        {
            var raves = new List<Rave>();

            using (var connection = GetConnection())
            {
                connection.Open();

                string sql = @"SELECT R.* 
                       FROM Rave R
                       INNER JOIN UserRave UR ON R.ID = UR.Rave_ID
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

            return raves;
        }


        public void RemoveRaveFromAttendingList(int userId, int raveId)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string sql = @"DELETE FROM UserRave 
                       WHERE User_ID = @User_ID AND Rave_ID = @Rave_ID";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@User_ID", userId);
                    command.Parameters.AddWithValue("@Rave_ID", raveId);

                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
