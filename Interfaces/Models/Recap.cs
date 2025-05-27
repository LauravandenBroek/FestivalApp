namespace Interfaces.Models
{
    public class Recap
    {
        private int _Id;
        private string _Description;
        private string _Rave;
        //private User _User;
        private List<byte[]> _Album;


        public int Id { get { return _Id; } set { _Id = value; } }
        public string Description { get { return _Description;  } set { _Description = value; } }
        public string Rave { get { return _Rave; } set { _Rave = value; } }
        public List<byte[]> Album { get { return _Album; } set { _Album = value; } }
        public Recap() { }
        
        public Recap (int id, string description, string rave, List<byte[]> album)
        {
            _Id = id;
            _Description = description;
            _Rave = rave;
            _Album = album;
        }       
    }
}
