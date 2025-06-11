using Interfaces;
using Interfaces.Models;
using Microsoft.AspNetCore.SignalR;

namespace TestFestivalApp.RepositoryMocks
{
    public class RaveWishlistRepositoryMock : IRaveWishlistRepository
    {

        public List<(int UserId, int RaveId)> WishlistEntries { get; set; } = new List<(int UserId, int RaveId)>
        {
            (1, 5),
            (2, 3),
            (4, 10)
        };

        public void AddRaveToWishlist(int userId, int raveId)
        {
            
                WishlistEntries.Add((userId, raveId));
            
        }

        public void RemoveRaveFromWishlist(int userId, int raveId)
        {
            WishlistEntries.RemoveAll(entry => entry.UserId == userId && entry.RaveId == raveId);
        }

        public List<Rave> GetRaveWishlistByUserId(int userId, int limit = 0)
        {
            var raveIds = WishlistEntries
                .Where(entry => entry.UserId == userId)
                .Select(entry => entry.RaveId);

            if (limit > 0)
            {
                raveIds = raveIds.Take(limit);
            }

            return raveIds.Select(id => new Rave { Id = id, Name = $"Rave {id}" }).ToList();
        }
    }
}
