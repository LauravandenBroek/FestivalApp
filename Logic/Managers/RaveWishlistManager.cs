using Interfaces.Models;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Managers
{
    public class RaveWishlistManager
    {
        private readonly IRaveWishlistRepository _raveWishlistRepository;

        public RaveWishlistManager(IRaveWishlistRepository RaveWishlistRepository)
        {
            _raveWishlistRepository = RaveWishlistRepository;
        }

        public async Task AddRaveToWishList(int UserId, int RaveId)
        {
            _raveWishlistRepository.AddRaveToWishlist(UserId, RaveId);
        }

        public List<Rave> GetRaveWishlistByUserId(int UserId)
        {
            return _raveWishlistRepository.GetRaveWishlistByUserId(UserId);
        }

        public List<Rave> Get5WishlistRavesByUserId(int userId)
        {
            return _raveWishlistRepository.GetRaveWishlistByUserId(userId, 5);
        }

        public bool IsRaveOnUserWishlist(int UserId, int RaveId)
        {
            var RaveWishlist = GetRaveWishlistByUserId(UserId);
            return RaveWishlist.Any(r => r.Id == RaveId);
        }

        public async Task RemoveRaveFromWishlist(int UserId, int RaveId)
        {
            _raveWishlistRepository.RemoveRaveFromWishlist(UserId, RaveId);
        }
    }
}
