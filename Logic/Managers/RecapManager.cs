using Interfaces;
using Interfaces.Models;
using Logic.ViewModels;
using Logic.Exceptions;


namespace Logic.Managers
{
    public class RecapManager
    {
        private readonly IRecapRepository _recapRepository;
            
        public RecapManager(IRecapRepository recapRepository)
        {
            _recapRepository = recapRepository;
        }

        public void AddRecap(RecapViewModel input, int userId)
        {
            if (string.IsNullOrWhiteSpace(input.Rave) || string.IsNullOrWhiteSpace(input.Description))
            { 
                throw new ValidationException("Please fill in all the fields.");
            }

            var newRecap = new Recap
            {
                Rave = input.Rave,
                Description = input.Description,
                Album = input.Album
            };

            _recapRepository.AddRecap(newRecap, userId);
        }

        public List<Recap> GetRecapsByUserId(int userId, int limit = 0)
        {
            return _recapRepository.GetRecapsByUserId(userId, limit);
        }

        public Recap ? GetRecapById(int id)
        {
            return _recapRepository.GetRecapById(id);
        }

        public void UpdateRecap(RecapViewModel input)
        {
            var updatedRecap = new Recap
            {
                Id = input.Id,
                Rave = input.Rave,
                Description = input.Description,
                Album = input.Album
            };
            _recapRepository.UpdateRecap(updatedRecap);
        }
    }
}
