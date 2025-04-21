using FestivalApp.Data;
using FestivalApp.Interfaces;
using FestivalApp.Models;
using System.Xml.Serialization;

namespace FestivalApp.Managers
{
    public class RaveManager
    {
        private readonly RaveRepository _raveRepository;

        public RaveManager(RaveRepository raveRepository)
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
    }
}
