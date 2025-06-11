using Interfaces.Models;
using Interfaces;
using Logic.Exceptions;


namespace Logic.Managers
{
    public class RaveWishlistManager
    {
        private readonly IRaveWishlistRepository _raveWishlistRepository;

        public RaveWishlistManager(IRaveWishlistRepository RaveWishlistRepository)
        {
            _raveWishlistRepository = RaveWishlistRepository;
        }

        public async Task AddRaveToWishList(int UserId, int RaveId) { 

             if (UserId <= 0 || RaveId <= 0)
             {
                throw new ValidationException("Invalid user or rave ID.");
             }
             if (IsRaveOnUserWishlist(UserId, RaveId))
             {
                throw new ValidationException("Rave is already on your wishlist");
             }
                
            _raveWishlistRepository.AddRaveToWishlist(UserId, RaveId);
        }

        public List<Rave> GetRaveWishlistByUserId(int UserId, int limit = 0)
        {
            return _raveWishlistRepository.GetRaveWishlistByUserId(UserId, limit);
        }

        public bool IsRaveOnUserWishlist(int UserId, int RaveId)
        {
            var RaveWishlist = GetRaveWishlistByUserId(UserId);
            return RaveWishlist.Any(r => r.Id == RaveId);
        }

        public async Task RemoveRaveFromWishlist(int UserId, int RaveId)
        {
            if (UserId <= 0 || RaveId <= 0)
            {
                throw new ValidationException("Invalid user or rave ID.");
            }
            if (!IsRaveOnUserWishlist(UserId, RaveId))
            {
                throw new ValidationException("Rave is not on your Wishlist");
            }
            _raveWishlistRepository.RemoveRaveFromWishlist(UserId, RaveId);
        }
    }
}
