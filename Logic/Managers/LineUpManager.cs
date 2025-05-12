using Interfaces;
using Interfaces.Models;

namespace Logic.Managers
{
    public class LineUpManager
    {

        private readonly ILineUpRepository _lineUpRepository;
        public LineUpManager(ILineUpRepository lineUpRepository)
        {
            _lineUpRepository = lineUpRepository;
        }

        public void AddLineUp(LineUp lineUp)
        {
            _lineUpRepository.AddLineUp(lineUp);
        }

        public List<LineUp> GetLineUpByRaveId(int raveId)
        {
            return _lineUpRepository.GetLineUpByRaveId(raveId);
        }
    }
}
