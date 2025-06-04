using Interfaces;
using Interfaces.Models;
using Logic.ViewModels;
using System.ComponentModel.DataAnnotations;


namespace Logic.Managers
{
    public class RecapManager
    {
        private readonly IRecapRepository _recapRepository;
            
        public RecapManager(IRecapRepository recapRepository)
        {
            _recapRepository = recapRepository;
        }

        public void AddRecap(AddRecapViewModel input, int UserId)
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

            _recapRepository.AddRecap(newRecap, UserId);
        }
    }
}
