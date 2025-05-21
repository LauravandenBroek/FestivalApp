using Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IAttendingRaveRepository
    {
        public void AddRaveToAttendingList(int UserId, int RaveId);
        public List<Rave> GetAttendingRavesByUserId(int userId, int limit = 0);
        public void RemoveRaveFromAttendingList(int userId, int raveId);
    }
}
