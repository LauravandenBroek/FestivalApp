using Interfaces.Models;
using Microsoft.Data.SqlClient;
using Interfaces;

namespace Data
{
    public class LineUpRepository : DatabaseConnection, ILineUpRepository
    {
        public LineUpRepository(string connectionString) : base(connectionString)
        {

        }

        public void AddLineUp(LineUp lineUp)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string sql = @"INSERT INTO Line_up (Rave_ID, Artist_ID, Start_time, End_time, Stage) 
                           VALUES (@Rave_ID, @Artist_ID, @Start_time, @End_Time, @Stage)";

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
        }

        public List<LineUp> GetLineUpByRaveId(int raveId)
        {
            List<LineUp> lineUps = new List<LineUp>();

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string sql = @"SELECT l.Id, l.Start_time, l.End_time, l.Stage, 
                              a.Id AS ArtistId, a.Name AS ArtistName,
                              r.Id AS RaveId, r.Name AS RaveName
                       FROM Line_up l
                       JOIN Artist a ON l.Artist_ID = a.Id
                       JOIN Rave r ON l.Rave_ID = r.Id
                       WHERE l.Rave_ID = @Rave_ID";

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

            return lineUps;
        }

    }
}
