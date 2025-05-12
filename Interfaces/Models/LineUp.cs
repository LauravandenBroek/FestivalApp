namespace Interfaces.Models
{
    public class LineUp
    {
        private int _Id;
        private Artist _Artist;
        private Rave _Rave;
        private DateTime _StartTime;
        private DateTime _EndTime;
        private string _Stage;

        public int Id { get { return _Id; } set { _Id = value; } }
        public Artist Artist { get { return _Artist; } set { _Artist = value; } }
        public Rave Rave { get { return _Rave; } set { _Rave = value; } }
        public DateTime StartTime { get { return _StartTime; } set { _StartTime = value; } }
        public DateTime EndTime { get { return _EndTime; } set { _EndTime = value; } }
        public string Stage { get { return _Stage; } set { _Stage = value; } }

        public LineUp() {}

        public LineUp(int id, Artist artist, Rave rave, DateTime startTime, DateTime endTime, string stage) 
        { 
            _Id = id;
            _Artist = artist;
            _Rave = rave;
            _StartTime = startTime;
            _EndTime = endTime;
            _Stage = stage;
        }
    }
}
