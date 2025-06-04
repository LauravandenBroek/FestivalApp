using Interfaces;
using Interfaces.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Logic.Managers
{
    public class FavoriteArtistManager
    {
        private readonly IFavoriteArtistRepository _favoriteArtistRepository;

        public FavoriteArtistManager(IFavoriteArtistRepository favoriteArtistRepository)
        {
            _favoriteArtistRepository=favoriteArtistRepository;
        }

        public async Task AddArtistToFavorites(int UserId, int ArtistId)
        {
            if (UserId <= 0 || ArtistId <= 0)
            {
                throw new ValidationException("Invalid user or artist ID.");
            }
            if (IsArtistOnFavorites(UserId, ArtistId))
            {
                throw new ValidationException("Artist is already on favorite list");
            }
            _favoriteArtistRepository.AddArtistToFavorites(UserId, ArtistId);
        }

        public List<Artist> GetFavoriteArtistsByUserId(int UserId, int limit = 0)
        {
            return _favoriteArtistRepository.GetFavoriteArtistsByUserId(UserId, limit);
        }


        public bool IsArtistOnFavorites(int UserId, int ArtistId)
        {
            var FavoriteArtists = GetFavoriteArtistsByUserId(UserId);
            return FavoriteArtists.Any(r => r.Id == ArtistId);
        }

        public async Task RemoveArtistFromFavorites(int UserId, int ArtistId)
        {
            if (UserId <= 0 || ArtistId <= 0)
            {
                throw new ValidationException("Invalid user or artist ID.");
            }
            if (!IsArtistOnFavorites(UserId, ArtistId))
            {
                throw new ValidationException("Artist is not on favorite list");
            }
            _favoriteArtistRepository.RemoveArtistFromFavorites(UserId, ArtistId);
        }
    }
}
