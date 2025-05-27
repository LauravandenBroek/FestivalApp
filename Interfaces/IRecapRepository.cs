using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IRecapRepository
    {
        public void AddRecap(Recap recap, int UserId);
        public List<Recap> GetRecapsByUserId(int UserId);
        public Recap? GetRecapById(int id);
        public void UpdateRecap(Recap recap);
        public void DeleteRecap(int id);
    }
}
