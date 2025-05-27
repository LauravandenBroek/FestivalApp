using Interfaces;
using Interfaces.Models;

namespace Logic.Managers
{
    public class RecapManager
    {
        private readonly IRecapRepository _recapRepository;
            
        public RecapManager(IRecapRepository recapRepository)
        {
            _recapRepository = recapRepository;
        }

        public void AddRecap(Recap recap, int UserId)
        {
            _recapRepository.AddRecap(recap, UserId);
        }
    }
}
