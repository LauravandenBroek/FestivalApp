using Interfaces;
using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            _attendingRaveRepository.AddRaveToAttendingList(UserId, RaveId);
        }

        public List<Rave> GetAttendingRavesByUserId(int UserId)
        { 
            return _attendingRaveRepository.GetAttendingRavesByUserId(UserId);
        }

        public List<Rave> Get5AttendingRavesByUserId(int userId)
        {
            return _attendingRaveRepository.GetAttendingRavesByUserId(userId, 5);
        }

        public bool IsUserAttendingRave(int UserId, int RaveId)
        {
            var attendingRaves = GetAttendingRavesByUserId(UserId);
            return attendingRaves.Any(r => r.Id == RaveId);
        }

        public async Task RemoveRaveFromAttendingList (int UserId, int RaveId)
        {
           _attendingRaveRepository.RemoveRaveFromAttendingList(UserId, RaveId);
        }
    }
}
