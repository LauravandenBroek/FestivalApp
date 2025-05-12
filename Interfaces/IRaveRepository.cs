using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IRaveRepository
    {
        public void AddRave(Rave rave);
        public List<Rave> GetRaves();
        public Rave? GetRaveById(int id);
        public void UpdateRave(Rave rave);
        public void DeleteRave(int id);
        public List<Rave> GetUpcomingRaves(int count);
       
    }
}
