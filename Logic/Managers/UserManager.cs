using Interfaces;
using Interfaces.Models;
using BCrypt.Net;
using Logic.ViewModels;
using System.ComponentModel.DataAnnotations;


namespace Logic.Managers
{
    public class UserManager
    {
        private readonly IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

       public bool EmailExists(string email)
       {
            var existingUser = _userRepository.GetUserByEmail(email);
            return existingUser != null;
       }

        public void RegisterUser(RegisterViewModel input)
        {
            if (EmailExists(input.Email))
            {
                throw new ValidationException("Email is already registered.");
            }

            if (string.IsNullOrWhiteSpace(input.Name) || string.IsNullOrWhiteSpace(input.Email) || string.IsNullOrWhiteSpace(input.Password) || string.IsNullOrWhiteSpace(input.ConfirmPassword) || input.Birthdate == default)
            {
                throw new ValidationException("Please fill in all the fields");
            }
            if (input.Password.Length < 8)
            {
                throw new ValidationException("Password must be at least 8 characters long.");
            }

            if (!input.Password.Any(char.IsDigit))
            {
                throw new ValidationException("Password must contain at least one number.");
            }

            var newUser = new User
            {
                Name = input.Name,
                Email = input.Email,
                PasswordHash = input.Password,
                Birthdate = input.Birthdate,
                Role = "User"
            };
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newUser.PasswordHash);
            newUser.PasswordHash = hashedPassword;

            _userRepository.AddUser(newUser);
        }


        public User ValidateUser(string email, string password)
        {
            var user = _userRepository.GetUserByEmail(email);
            if (user == null) return null;

            var isValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            return isValid ? user : null;
        }

        public User? GetUserByEmail(string email)
        {
            return _userRepository.GetUserByEmail(email);
        }

        public bool IsAdmin(string email)
        {
            var user = _userRepository.GetUserByEmail(email);
            return user?.Role == "Admin";
        }
    }
}
