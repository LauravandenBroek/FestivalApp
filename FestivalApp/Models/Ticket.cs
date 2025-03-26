namespace FestivalApp.Models
{
    public class Ticket
    {
        private int _Id;
        private string _Name;
        private string _Ticket_image;
        private DateTime _End_date;
        private User _User;
        private Rave _Rave;

        public int Id { get { return _Id; } set { _Id = value; } }
        public string Name { get { return _Name; } set { _Name = value; } }
        public string Ticket_image { get { return _Ticket_image; } set { _Ticket_image = value; } }
        public DateTime End_date { get { return _End_date; } set { _End_date = value; } }
        public User User { get { return _User; } set { _User = value; } }
        public Rave Rave { get { return _Rave; } set { _Rave = value; } }

        public Ticket (int id, string name, string ticket_image, DateTime end_date, User user, Rave rave)
        {
            Id = id;
            Name = name;
            Ticket_image = ticket_image;
            End_date = end_date;
            User = user;
            Rave = rave;
        }
    }
}
