using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IRaveWishlistRepository
    {
        public void AddRaveToWishlist(int UserId, int RaveId);
        public List<Rave> GetRaveWishlistByUserId(int userId, int limit = 0);
        public void RemoveRaveFromWishlist(int userId, int raveId);

    }
}
