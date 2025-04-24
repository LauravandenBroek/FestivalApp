using FestivalApp.Data;
using FestivalApp.Models;


namespace FestivalApp.Managers
{
    public class UserManager
    {
        private readonly UserRepository _userRepository;

        public UserManager(UserRepository userRepository)
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
    }
}
