namespace FestivalApp.Models
{
    public class RecapPhoto
    {
        private int _Id;
        private string _Photo;

        public int Id { get { return _Id; } }
        public string Photo { get { return _Photo; } set { _Photo = value; } }

        public RecapPhoto(int id, string photo)
        {
            int _Id = id;
            string _Photo = photo;
        }
    }
}
