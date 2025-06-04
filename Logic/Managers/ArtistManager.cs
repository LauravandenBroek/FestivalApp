
using Interfaces;
using Interfaces.Models;
using Logic.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Logic.Managers

{
    public class ArtistManager

    {
        private readonly IArtistRepository _artistRepository;
        public ArtistManager(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public void AddArtist(AddArtistViewModel input)
        {
            if (string.IsNullOrWhiteSpace(input.Name) ||string.IsNullOrWhiteSpace(input.Nationality) || string.IsNullOrWhiteSpace(input.Genre) || string.IsNullOrWhiteSpace(input.Description) || input.Image == null || input.Image.Length == 0)
                    
            {
                throw new ValidationException("Please fill in all the fields.");
            }

            var newArtist = new Artist
            {
               
                Name = input.Name,
                Nationality = input.Nationality,
                Genre = input.Genre,
                Description = input.Description,
                Image = input.Image,
            };

            _artistRepository.AddArtist(newArtist);
        }

        public List<Artist> GetArtists()
        {
            return _artistRepository.GetArtists();
        }

        public Artist? GetArtistById(int id)
        {
            return _artistRepository.GetArtistById(id);
        }

        public void UpdateArtist(EditArtistViewModel input)
        {
            if (string.IsNullOrWhiteSpace(input.Name) ||string.IsNullOrWhiteSpace(input.Nationality) || string.IsNullOrWhiteSpace(input.Genre) || string.IsNullOrWhiteSpace(input.Description) || input.Image == null || input.Image.Length == 0)
            {
                throw new ValidationException("Please fill in all the fields.");
            }

            var updatedArtist = new Artist
            {
                Id = input.Id,
                Name = input.Name,
                Nationality = input.Nationality,
                Genre = input.Genre,
                Description = input.Description,
                Image = input.Image
            };

            _artistRepository.UpdateArtist(updatedArtist);
        }

        public void DeleteArtist(int id)
        {
            _artistRepository.DeleteArtist(id);
        }

        public List<Artist> GetArtistsPaged(int page, int pageSize)
        {
            return _artistRepository.GetArtistsPaged(page, pageSize);
        }

        public int GetTotalArtistCount()
        {
            return GetArtists().Count();
        }

    }
}