using Interfaces;
using Interfaces.Models;
using Logic.ViewModels;
using Logic.Exceptions;


namespace Logic.Managers
{
    public class LineUpManager
    {

        private readonly ILineUpRepository _lineUpRepository;
        public LineUpManager(ILineUpRepository lineUpRepository)
        {
            _lineUpRepository = lineUpRepository;
        }

        public void AddLineUp(LineUpViewModel input, Rave rave, Artist artist)
        {
            if (rave == null || artist == null || string.IsNullOrWhiteSpace(input.Stage))
            {
                throw new ValidationException("Please fill in all the fields.");
            }

            if (input.Stage.Length > 50) 
            {
                throw new ValidationException("Stage can be max 50 characters.");
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
