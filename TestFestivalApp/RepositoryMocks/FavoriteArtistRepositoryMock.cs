using Interfaces;
using Interfaces.Models;


namespace TestFestivalApp.RepositoryMocks
{
    public class FavoriteArtistRepositoryMock : IFavoriteArtistRepository
    {
        public List<(int UserId, int ArtistId)> FavoriteArtists { get; set; } = new List<(int UserId, int ArtistId)>
        {
            (1, 5),
            (2, 3),
            (4, 10)
        };

        public void AddArtistToFavorites(int userId, int artistId)
        {
            FavoriteArtists.Add((userId, artistId));

        }

        public List<Artist> GetFavoriteArtistsByUserId(int userId, int limit = 0)
        {
            var artistIds = FavoriteArtists
                .Where(entry => entry.UserId == userId)
                .Select(entry => entry.ArtistId);

            if (limit > 0)
            {
                artistIds = artistIds.Take(limit);
            }

            return artistIds.Select(id => new Artist { Id = id, Name = $"Artist {id}" }).ToList();
        }

        public void RemoveArtistFromFavorites(int userId, int artistId)
        {
            FavoriteArtists.RemoveAll(entry => entry.UserId == userId && entry.ArtistId == artistId);
        }
    }
}
