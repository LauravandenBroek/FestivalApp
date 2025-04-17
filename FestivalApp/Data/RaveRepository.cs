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
    }
}
