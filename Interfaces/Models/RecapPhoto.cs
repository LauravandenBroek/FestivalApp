namespace Interfaces.Models
{
    public class RecapPhoto
    {
        private int _Id;
        private byte[] _Photo;

        public int Id { get { return _Id; } }
        public byte[] Photo { get { return _Photo; } set { _Photo = value; } }

        public RecapPhoto(int id, byte[] photo)
        {
            int _Id = id;
            byte[] _Photo = photo;
        }
    }
}
