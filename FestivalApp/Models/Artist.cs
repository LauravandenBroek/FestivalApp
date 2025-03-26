namespace FestivalApp.Models
{
    public class Artist
    {
        private int _Id; 
        private string _Name;
        private string _Nationality;
        private string _Genre;
        private string _Description;
        private string _Image;

        public int Id { get { return _Id; } }
        public string Name { get { return _Name; } set { _Name = value; } }
        public string Nationality { get { return _Nationality; } set { _Nationality = value; } }
        public string Genre { get { return _Genre; } set { _Genre = value; } }
        public string Description { get { return _Description; } set { _Description = value; } }
        public string Image { get { return _Image; } set { _Image = value; } }

        public Artist (int id, string name, string nationality, string genre, string description, string image)
        {
            _Id = id;
            _Name = name;
            _Nationality = nationality;
            _Genre = genre;
            _Description = description;
            _Image = image;
        }
    }
}
