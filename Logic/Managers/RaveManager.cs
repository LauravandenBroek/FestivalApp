using Interfaces;
using Interfaces.Models;
using System.Xml.Serialization;

namespace Logic.Managers
{
    public class RaveManager
    {
        private readonly IRaveRepository _raveRepository;

        public RaveManager(IRaveRepository raveRepository)
        {
            _raveRepository = raveRepository;
        }
        public void AddRave(Rave rave)
        {
            _raveRepository.AddRave(rave);
        }

        public List<Rave> GetRaves()
        {
            return _raveRepository.GetRaves();
        }

        public void UpdateRave(Rave rave)
        {
            _raveRepository.UpdateRave(rave);
        }

        public Rave? GetRaveById(int id)
        {
            return _raveRepository.GetRaveById(id);
        }

        public void DeleteRave(int id)
        {
            _raveRepository.DeleteRave(id);
        }

        public List<Rave> GetUpcomingRaves(int count)
        {
            return _raveRepository.GetUpcomingRaves(count);
        }
    }
}
