
using Interfaces;
using Interfaces.Models;
using Logic.ViewModels;
using Logic.Exceptions;

namespace Logic.Managers

{
    public class ArtistManager

    {
        private readonly IArtistRepository _artistRepository;
        public ArtistManager(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public void AddArtist(ArtistViewModel input)
        {
            ValidateArtistInput(input);

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

        public List<Artist> GetArtists(int limit = 0)
        {
            return _artistRepository.GetArtists(limit);
        }

        public Artist? GetArtistById(int id)
        {
            return _artistRepository.GetArtistById(id);
        }

        public void UpdateArtist(ArtistViewModel input)
        {
            ValidateArtistInput(input);


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

        private void ValidateArtistInput(ArtistViewModel input)
        {
            if (string.IsNullOrWhiteSpace(input.Name) || string.IsNullOrWhiteSpace(input.Nationality) || string.IsNullOrWhiteSpace(input.Genre) || string.IsNullOrWhiteSpace(input.Description) || input.Image == null || input.Image.Length == 0)
            {
                throw new ValidationException("Please fill in all the fields.");
            }

            if (input.Name.Length > 50)
            {
                throw new ValidationException("Name can be max 50 characters.");
            }

            if (input.Nationality.Length > 50)
            {
                throw new ValidationException("Nationality can be max 50 characters.");
            }

            if (input.Genre.Length > 50)
            {
                throw new ValidationException("Genre can be max 50 characters.");
            }

            if (input.Description.Length > 800)
            {
                throw new ValidationException("Description can be max 800 characters.");
            }
        }
    }
}