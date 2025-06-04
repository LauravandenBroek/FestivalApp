using Interfaces;
using Interfaces.Models;
using Logic.ViewModels;
using System.ComponentModel.DataAnnotations;


namespace Logic.Managers
{
    public class LineUpManager
    {

        private readonly ILineUpRepository _lineUpRepository;
        public LineUpManager(ILineUpRepository lineUpRepository)
        {
            _lineUpRepository = lineUpRepository;
        }

        public void AddLineUp(AddLineUpViewModel input, Rave rave, Artist artist)
        {
            if (rave == null || artist == null || string.IsNullOrWhiteSpace(input.Stage))
            {
                throw new ValidationException("Please fill in all the fields.");
            }
            var newLineUp = new LineUp
            {
                Rave = rave,
                Artist = artist,
                StartTime = input.StartTime,
                EndTime = input.EndTime,
                Stage = input.Stage
            };

            _lineUpRepository.AddLineUp(newLineUp);
        }

        public List<LineUp> GetLineUpByRaveId(int raveId)
        {
            return _lineUpRepository.GetLineUpByRaveId(raveId);
        }
    }
}
