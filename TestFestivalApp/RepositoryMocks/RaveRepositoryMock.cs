using Interfaces;
using Interfaces.Models;


namespace TestFestivalApp.RepositoryMocks
{
    public class RaveRepositoryMock : IRaveRepository
    {
        public List<Rave> Raves = new List<Rave>
        {
            new Rave
            {
                Id = 1,
                Name = "Bass Inferno",
                Location = "Amsterdam",
                Date = new DateOnly(2025, 7, 15),
                Website = "https://bassinferno.nl",
                Minimum_age = 18,
                Ticket_link = "https://tickets.bassinferno.nl",
                Description = "An energetic bass-heavy rave with top DJs.",
                Time = "22:00 - 06:00",
                Image = new byte[] { 1, 2, 3 }
            },
            new Rave
            {
                Id = 2,
                Name = "Techno Tunnel",
                Location = "Rotterdam",
                Date = new DateOnly(2025, 8, 5),
                Website = "https://technotunnel.com",
                Minimum_age = 21,
                Ticket_link = "https://tickets.technotunnel.com",
                Description = "Underground techno all night long.",
                Time = "23:00 - 07:00",
                Image = new byte[] { 4, 5, 6 }
            }
        };

        public void AddRave(Rave rave)
        {
            rave.Id = Raves.Any() ? Raves.Max(r => r.Id) + 1 : 1;
            Raves.Add(rave);
        }

        public void DeleteRave(int id)
        {
            var rave = Raves.FirstOrDefault(r => r.Id == id);
            if (rave != null)
            Raves.Remove(rave);
        }

        public Rave? GetRaveById(int id)
        {
            return Raves.FirstOrDefault(r => r.Id == id);
        }

        public List<Rave> GetRaves()
        {
            return Raves.ToList();
        }

        public List<Rave> GetUpcomingRaves(int count)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            return Raves
                .Where(r => r.Date >= today)
                .OrderBy(r => r.Date)
                .Take(count)
                .ToList();
        }

        public List<Rave> GetRavesPaged(int page, int pageSize)
        {
            return Raves
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public void UpdateRave(Rave updatedRave)
        {
            var existingRave = Raves.FirstOrDefault(r => r.Id == updatedRave.Id);
            if (existingRave != null)
            {
                existingRave.Name = updatedRave.Name;
                existingRave.Location = updatedRave.Location;
                existingRave.Date = updatedRave.Date;
                existingRave.Website = updatedRave.Website;
                existingRave.Minimum_age = updatedRave.Minimum_age;
                existingRave.Ticket_link = updatedRave.Ticket_link;
                existingRave.Description = updatedRave.Description;
                existingRave.Time = updatedRave.Time;
                existingRave.Image = updatedRave.Image;
            }
        }
    }
}
