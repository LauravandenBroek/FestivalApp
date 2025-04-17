using FestivalApp.Data;
using FestivalApp.Interfaces;
using FestivalApp.Models;
namespace FestivalApp.Managers
{
    public class ArtistManager

    {
        //private readonly ArtistRepository _artistRepository;
        private readonly IArtistRepository _artistRepository;
        public ArtistManager(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public void AddArtist(Artist artist)
        {
            _artistRepository.AddArtist(artist);
        }

        public List<Artist> GetArtists()
        {
            return _artistRepository.GetArtists();
        }

        public Artist? GetArtistById(int id)
        {
            return _artistRepository.GetArtistById(id);
        }

        public void UpdateArtist(Artist artist)
        {
            _artistRepository.UpdateArtist(artist);
        }

        public void DeleteArtist(int id)
        {
            _artistRepository.DeleteArtist(id);
        }

    }
}