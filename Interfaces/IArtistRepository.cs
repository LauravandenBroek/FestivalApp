using Interfaces.Models;
namespace Interfaces
{
    public interface IArtistRepository
    {
        public void AddArtist(Artist artist);
        public List<Artist> GetArtists(int limit = 0);
        public Artist GetArtistById(int id);
        public void UpdateArtist(Artist artist);
        public void DeleteArtist(int id);
        public List<Artist> GetArtistsPaged(int page, int PageSize);
    }
}
