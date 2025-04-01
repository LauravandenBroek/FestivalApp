namespace FestivalApp.Models
{
    public class Rave
    {
        private int _Id;
        private string _Name;
        private string _Location;
        private DateOnly _Date;
        private string _Website;
        private int _Minimum_age;
        private string _Ticket_link;
        private string _Time;
        private byte[] _Image; 

        public int Id { get { return _Id; } }
        public string Name { get { return _Name; } set { _Name = value; } }
        public string Location { get { return _Location; } set { _Location = value; } }
        public DateOnly Date { get { return _Date; } set { _Date = value; } }
        public string Website { get { return _Website; } set { _Website = value; } }
        public int Minimum_age { get { return _Minimum_age; } set { _Minimum_age = value; } }
        public string Ticket_link { get { return _Ticket_link; } set { _Ticket_link = value; } }
        public string Time { get { return _Time; } set { _Time = value; } }
        public byte[] Image { get { return _Image; } set { _Image = value; } }

        public Rave (int id, string name, string location, DateOnly date, string website, int minimum_age, string ticket_link, string time, byte[] image)
        {
            _Id = id;
            _Name = name;
            _Location = location;
            _Date = date;
            _Website = website;
            _Minimum_age = minimum_age;
            _Ticket_link = ticket_link;
            _Time = time;
            _Image = image;
        }

        public Rave(int id, string name, DateOnly date, byte[] image)
        {
            _Id = id;
            _Name = name;
            _Date = date;
            _Image = image;
        }

    }
}
