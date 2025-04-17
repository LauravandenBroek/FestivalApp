using FestivalApp.Models;
namespace FestivalApp.Interfaces
{
    public interface IArtistRepository
    {
        public void AddArtist(Artist artist);
        public List<Artist> GetArtists();
        public Artist GetArtistById(int id);
        public void UpdateArtist(Artist artist);
        public void DeleteArtist(int id);

    }
}
