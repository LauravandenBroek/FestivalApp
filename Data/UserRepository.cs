using Microsoft.Identity.Client;
using Interfaces.Models;
using Microsoft.Data.SqlClient;
using Interfaces;
using Logic.Exceptions;
using Microsoft.Extensions.Logging;


namespace Data
{
    public class UserRepository : DatabaseConnection, IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;
        public UserRepository(ILogger<UserRepository> logger, string connectionString) : base(connectionString)
        {
            _logger = logger;
        }

        public void AddUser(User user)
        {
            SqlConnection connection = null;

            try
            {
                connection = GetConnection();

                try
                {
                    connection.Open();
                }
                catch (SqlException ex)
                {
                    _logger.LogError(ex, "No database connection");
                    throw new TemporaryDatabaseException();
                }

                try
                {
                    string sql = @"INSERT INTO [User] (Name, Email, Password, Birthdate, Role) 
                           VALUES (@Name, @Email, @Password, @Birthdate, @Role)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Name", user.Name);
                        command.Parameters.AddWithValue("@Email", user.Email);  // @ toegevoegd
                        command.Parameters.AddWithValue("@Password", user.PasswordHash);
                        command.Parameters.AddWithValue("@Birthdate", user.Birthdate);
                        command.Parameters.AddWithValue("@Role", user.Role);

                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    _logger.LogError(ex, "Failed to insert user data into the database.");
                    throw new PersistentDatabaseException();
                }
            }
            finally
            {
                connection?.Close();
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
