using Interfaces;
using Interfaces.Models;
using BCrypt.Net;
using Logic.ViewModels;


namespace Logic.Managers
{
    public class UserManager
    {
        private readonly IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository=userRepository;
        }

       public bool EmailExists(string email)
        {
            var existingUser = _userRepository.GetUserByEmail(email);
            return existingUser != null;
        }

        public void RegisterUser(RegisterViewModel Input)
        {
            var newUser = new User
            {
                Name = Input.Name,
                Email = Input.Email,
                PasswordHash = Input.Password,
                Birthdate = Input.Birthdate,
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
