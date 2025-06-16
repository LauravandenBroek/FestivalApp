using Interfaces.Models;

namespace Interfaces
{
    public interface IRaveRepository
    {
        public void AddRave(Rave rave);
        public List<Rave> GetRaves(int limit = 0);
        public Rave? GetRaveById(int id);
        public void UpdateRave(Rave rave);
        public void DeleteRave(int id);
        public List<Rave> GetUpcomingRaves(int count);
        public List<Rave> GetRavesPaged(int page, int pageSize);
       
    }
}
