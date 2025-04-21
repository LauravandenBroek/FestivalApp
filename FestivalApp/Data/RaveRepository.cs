using FestivalApp.Models;
using Microsoft.Data.SqlClient;

namespace FestivalApp.Data
{
    public class RaveRepository : DatabaseConnection
    {
        public RaveRepository(string connectionString) : base(connectionString)
        {
        }

        public void AddRave(Rave rave)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
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
        }

        public List<Rave> GetRaves()
        {
            List<Rave> raves = new List<Rave>();

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string sql = "SELECT ID, Name, Location, Date, Website, Min_age, Ticket_link, Description, Time, Image FROM Rave";

                using var command = new SqlCommand(sql, connection);
                using var reader = command.ExecuteReader();
                {
                    while (reader.Read())
                    {

                        byte[] image = reader.IsDBNull(reader.GetOrdinal("Image"))
                        ? null
                        : (byte[])reader["Image"];

                        raves.Add(new Rave(
                            reader.GetInt32(0), // ID
                            reader.GetString(1), // Name
                            reader.GetString(2), // Nationality
                            DateOnly.FromDateTime(reader.GetDateTime(3)), // Date //DateOnly is relatief nieuw en het wordt nog niet automatisch ondersteund. Daarom haal ik het op als DateTime en zet ik het hier om naar een DateOnly.
                            reader.GetString(4), // Website
                            reader.GetInt32(5), // Min_age
                            reader.GetString(6), //Ticket_link
                            reader.GetString(7), // Description
                            reader.GetString(8), // Time
                            image
                        ));
                    }
                } 
            }
            return raves;
        }

       
        public Rave? GetRaveById(int id)
        {
            using var connection = GetConnection();
            connection.Open();

            string sql = "SELECT Id, Name, Location, Date, Website, Min_age, Ticket_Link, Description, Time, Image FROM Rave WHERE id = @Id";

            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", id);

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new Rave(
                    reader.GetInt32(0), // ID
                    reader.GetString(1), // Name
                    reader.GetString(2), // Nationality
                    DateOnly.FromDateTime(reader.GetDateTime(3)), // Date //DateOnly is relatief nieuw en het wordt nog niet automatisch ondersteund. Daarom haal ik het op als DateTime en zet ik het hier om naar een DateOnly.
                    reader.GetString(4), // Website
                    reader.GetInt32(5), // Min_age
                    reader.GetString(6), //Ticket_link
                    reader.GetString(7), // Description
                    reader.GetString(8), // Time
                    reader["Image"] as byte[] // Image
                );
            }

            return null;
        }
        public void UpdateRave(Rave rave)
        {
            using var connection = GetConnection();
            connection.Open();


            string sql = @"UPDATE Rave SET Name = @Name, Location = @Location, Date = @Date, Website = @Website, Min_age = @Min_age, Ticket_link = @Ticket_link, Description = @Description, Time = @Time, Image = @Image WHERE Id = @ID";

            using var command = new SqlCommand(sql, connection);
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

        public void DeleteRave(int id)
        {
            using var connection = GetConnection();
            connection.Open();

            string sql = "DELETE FROM Rave WHERE id = @ID";

            using var command = new SqlCommand (sql, connection);
            command.Parameters.AddWithValue("@ID", id);
            command.ExecuteNonQuery ();
        }
    }

  
}
