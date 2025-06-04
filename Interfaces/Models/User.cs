namespace Interfaces.Models
{
    public class User
    {
        private int _Id;
        private string _Name;
        private string _Email;
        private string _PasswordHash;
        private DateTime _Birthdate;
        private string _Role;
      

        public int Id { get { return _Id; } set { _Id = value; } }
        public string Name { get { return _Name; } set {  _Name = value; } }
        public string Email { get { return _Email; } set { _Email = value; } }
        public string PasswordHash { get { return _PasswordHash; } set { _PasswordHash = value; } }
        public DateTime Birthdate { get { return _Birthdate; } set { _Birthdate = value; } }
        public string Role { get { return _Role; } set { _Role = value; } }

        public User (int id, string name, string email, string passwordHash, DateTime birthdate, string role)
        {
            _Id = id;
            _Name = name;
            _Email = email;
            _PasswordHash = passwordHash;
            _Birthdate = birthdate;
            _Role = role;
        }

        public User () { }
    }
}
