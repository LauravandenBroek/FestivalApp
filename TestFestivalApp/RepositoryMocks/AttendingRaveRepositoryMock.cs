using Interfaces;
using Interfaces.Models;

namespace TestFestivalApp.RepositoryMocks
{
    public class AttendingRaveRepositoryMock : IAttendingRaveRepository
    {

        public List<(int UserId, int RaveId)> AttendingRaves { get; set; } = new List<(int UserId, int RaveId)>
        {
            (1, 5),
            (2, 3),
            (4, 10)
        };
        public void AddRaveToAttendingList(int UserId, int RaveId)
        {
            AttendingRaves.Add((UserId, RaveId));
        }

        public List<Rave> GetAttendingRavesByUserId(int userId, int limit = 0)
        {
            var raveIds = AttendingRaves
                .Where(entry => entry.UserId == userId)
                .Select(entry => entry.RaveId);

            if (limit > 0)
            {
                raveIds = raveIds.Take(limit);
            }

            return raveIds.Select(id => new Rave { Id = id, Name = $"Rave {id}" }).ToList();
        }

        public void RemoveRaveFromAttendingList(int userId, int raveId)
        {
            AttendingRaves.RemoveAll(entry => entry.UserId == userId && entry.RaveId == raveId);
        }
    }
}
