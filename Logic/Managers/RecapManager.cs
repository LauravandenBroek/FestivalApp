using Interfaces;

namespace Logic.Managers
{
    public class RecapManager
    {
        private readonly IRecapRepository RecapRepository;
            
        public RecapManager(IRecapRepository recapRepository)
        {
            RecapRepository = recapRepository;
        }
    }
}
