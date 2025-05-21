using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class FavoriteArtistRepository : DatabaseConnection
    {
        public FavoriteArtistRepository(string connectionString) : base(connectionString)
        {
        }
    }
}
