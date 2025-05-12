using Interfaces;
using Interfaces.Models;
using BCrypt.Net;


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

        public void RegisterUser(User user)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            user.PasswordHash = hashedPassword;

            _userRepository.AddUser(user);
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
