namespace FestivalApp.Models
{
    public class User
    {
        private int _Id;
        private string _Name;
        private string _Email;
        private DateTime _Birthdate;
        private string _Role;
        //private List<Artist> FavoriteArtist;
        private List <Rave> _AttendingRaves; 
        private List <Rave> _Wishlist;
        //private List <Ticket> TicketWallet;
        public int Id { get { return _Id; } set { _Id = value; } }

        public string Name { get { return _Name; } set {  _Name = value; } }

        public string Email { get { return _Email; } set { _Email = value; } }

        public DateTime Birthdate { get { return _Birthdate; } set { _Birthdate = value; } }

        public string Role { get { return _Role; } set { _Role = value; } }

        public List<Rave> AttendingRaves { get { return _AttendingRaves; } set { _AttendingRaves = value; } }

        public List<Rave> Wishlist { get { return _Wishlist; } set { _Wishlist = value; } }

        public User (int id, string name, string email, DateTime birthdate, string role)
        {
            _Id = id;
            _Name = name;
            _Email = email;
            _Birthdate = birthdate;
            _Role = role;
        }
    }
}
