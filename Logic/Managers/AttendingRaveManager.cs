using Interfaces;
using Interfaces.Models;
using Logic.Exceptions;

namespace Logic.Managers
{
   public class AttendingRaveManager
    {
        private readonly IAttendingRaveRepository _attendingRaveRepository;

        public AttendingRaveManager(IAttendingRaveRepository attendingRaveRepository)
        {
            _attendingRaveRepository = attendingRaveRepository;
        }   

        public async Task AddRaveToAttendingList(int userId, int raveId)
        {
            if (userId <= 0 || raveId <= 0)
            {
                throw new ValidationException("Invalid user or rave ID.");
            }
            if (IsUserAttendingRave(userId, raveId))
            {
                throw new ValidationException("User is already attending this rave.");
            }

            _attendingRaveRepository.AddRaveToAttendingList(userId, raveId);
        }

        public List<Rave> GetAttendingRavesByUserId(int userId, int limit = 0)
        { 
            return _attendingRaveRepository.GetAttendingRavesByUserId(userId, limit);
        }

        public bool IsUserAttendingRave(int UserId, int RaveId)
        {
            var attendingRaves = GetAttendingRavesByUserId(UserId);
            return attendingRaves.Any(r => r.Id == RaveId);
        }

        public async Task RemoveRaveFromAttendingList (int userId, int raveId)
        {
            if (userId <= 0 || raveId <= 0)
            {
                throw new ValidationException("Invalid user or rave ID.");
            }
            if (!IsUserAttendingRave(userId, raveId))
            {
                throw new ValidationException("User is not attending this rave.");
            }
            _attendingRaveRepository.RemoveRaveFromAttendingList(userId, raveId);
        }
    }
}
