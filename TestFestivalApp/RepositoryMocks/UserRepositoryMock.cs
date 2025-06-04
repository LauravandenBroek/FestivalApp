using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using Interfaces.Models;

namespace TestFestivalApp.RepositoryMocks
{
    public class UserRepositoryMock : IUserRepository
    {
        private List<User> Users = new List<User>()
        {
            new User() { Id = 1, Name = "Alice Johnson", Email = "alice@example.com", Birthdate = new DateTime(1995, 6, 12), PasswordHash = "hashedpassword123", Role = "User"},
            new User() { Id = 2, Name = "Bob Smith", Email = "bob@example.com", Birthdate = new DateTime(1990, 11, 23),PasswordHash = "hashedpassword456", Role = "Admin" },
            new User() { Id = 3, Name = "Charlie Brown", Email = "charlie@example.com", Birthdate = new DateTime(2000, 1, 5), PasswordHash = "hashedpassword789", Role = "User"}
        };

        public void AddUser(User user)
        {
            Users.Add(user);
        }

        public User? GetUserByEmail(string email)
        {
            return Users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

        }
    }
}
