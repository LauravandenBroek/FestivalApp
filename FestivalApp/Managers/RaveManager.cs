using FestivalApp.Data;
using FestivalApp.Models;

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
    }
}
