using Microsoft.Identity.Client;
using FestivalApp.Models;
using Microsoft.Data.SqlClient;

namespace FestivalApp.Data
{
    public class UserRepository  : DatabaseConnection
    {
        public UserRepository(string connectionString) : base(connectionString)
        {
        }

        public void AddUser(User user)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string sql = @"INSERT INTO [User] (Name, Email, Password, Birthdate, Role) 
                            VALUES(@Name, @Email, @Password, @Birthdate, @Role)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Name", user.Name);
                    command.Parameters.AddWithValue("Email", user.Email);
                    command.Parameters.AddWithValue("Password", user.PasswordHash);
                    command.Parameters.AddWithValue("Birthdate", user.Birthdate);
                    command.Parameters.AddWithValue("Role", user.Role);

                    command.ExecuteNonQuery();
                }
            }
        }

        public User? GetUserByEmail(string email)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string sql = "SELECT * FROM [User] WHERE Email = @Email";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User(
                                id: Convert.ToInt32(reader["Id"]),
                                name: reader["Name"].ToString(),
                                email: reader["Email"].ToString(),
                                passwordHash: reader["Password"].ToString(),
                                birthdate: Convert.ToDateTime(reader["Birthdate"]),
                                role: reader["Role"].ToString()
                            );
                        }
                    }
                }
            }
            return null;
        }

    }
}
