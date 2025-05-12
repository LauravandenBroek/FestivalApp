namespace Interfaces.Models
{
    public class Recap
    {
        private int _Id;
        private string _Name;
        private string _Description;
        private User _User;
        private Rave _Rave;

        public int Id { get { return _Id; } } 
        public string Name { get { return _Name; } }
        public string Description { get { return _Description; } }
        public User User { get { return _User; } }
        public Rave Rave { get { return _Rave; } }

        public Recap (int id, string name, string description, User user, Rave rave)
        {
            _Id = id;
            _Name = name;
            _Description = description;
            _User = user;
            _Rave = rave;
        }       
    }
}
