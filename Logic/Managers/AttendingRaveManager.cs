using Interfaces;
using Interfaces.Models;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;

namespace Logic.Managers
{
   public class AttendingRaveManager
    {
        private readonly IAttendingRaveRepository _attendingRaveRepository;

        public AttendingRaveManager(IAttendingRaveRepository attendingRaveRepository)
        {
            _attendingRaveRepository = attendingRaveRepository;
        }   

        public async Task AddRaveToAttendingList(int UserId, int RaveId)
        {
            if (UserId <= 0 || RaveId <= 0)
            {
                throw new ValidationException("Invalid user or rave ID.");
            }
            if (IsUserAttendingRave(UserId, RaveId))
            {
                throw new ValidationException("User is already attending this rave.");
            }

            _attendingRaveRepository.AddRaveToAttendingList(UserId, RaveId);
        }

        public List<Rave> GetAttendingRavesByUserId(int UserId, int limit = 0)
        { 
            return _attendingRaveRepository.GetAttendingRavesByUserId(UserId, limit);
        }

        public bool IsUserAttendingRave(int UserId, int RaveId)
        {
            var attendingRaves = GetAttendingRavesByUserId(UserId);
            return attendingRaves.Any(r => r.Id == RaveId);
        }

        public async Task RemoveRaveFromAttendingList (int UserId, int RaveId)
        {
            if (UserId <= 0 || RaveId <= 0)
            {
                throw new ValidationException("Invalid user or rave ID.");
            }
            if (!IsUserAttendingRave(UserId, RaveId))
            {
                throw new ValidationException("User is not attending this rave.");
            }
            _attendingRaveRepository.RemoveRaveFromAttendingList(UserId, RaveId);
        }
    }
}
