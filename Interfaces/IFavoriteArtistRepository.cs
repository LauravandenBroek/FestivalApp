using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IFavoriteArtistRepository
    {
        public void AddArtistToFavorites(int userId, int artistId);
        public List<Artist> GetFavoriteArtistsByUserId(int userId, int limit = 0);
        public void RemoveArtistFromFavorites(int userId, int artistId); 
    }
}
