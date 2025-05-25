using Interfaces;
using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

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
            _favoriteArtistRepository.RemoveArtistFromFavorites(UserId, ArtistId);
        }
    }
}
