using Interfaces;
using Interfaces.Models;
using Logic.ViewModels;

namespace TestFestivalApp.RepositoryMocks
{
    public class LineUpRepositoryMock : ILineUpRepository
    {
        public List<LineUp> LineUps { get; set; } = new List<LineUp>
        {
             new LineUp
             {
                Id = 1,
                Artist = new Artist
                {
                    Id = 1,
                    Name = "Charlotte de Witte",
                    Nationality = "Belgium",
                    Genre = "Techno",
                    Description = "Belgian DJ and record producer known for her dark and stripped-back techno style.",
                    Image = new byte[] { 1, 2, 3 } // voorbeeld image bytes
                },
                
                Rave = new Rave
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

                StartTime = new DateTime(2025, 7, 15, 22, 0, 0),
                EndTime = new DateTime(2025, 7, 16, 0, 0, 0),
                Stage = "Mainstage"
            },
            new LineUp
            {
                Id = 2,
                Artist = new Artist
                {
                    Id = 2,
                    Name = "Amelie Lens",
                    Nationality = "Belgium",
                    Genre = "Techno",
                    Description = "One of the most prominent DJs in the European techno scene.",
                    Image = new byte[] { 4, 5, 6 }
                },
                Rave = new Rave
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
                StartTime = new DateTime(2025, 7, 16, 0, 15, 0),
                EndTime = new DateTime(2025, 7, 16, 2, 0, 0),
                Stage = "Mainstage"
            },

            new LineUp
            {
                Id = 3,
                Artist = new Artist
                {
                    Id = 3,
                    Name = "Reinier Zonneveld",
                    Nationality = "Netherlands",
                    Genre = "Acid Techno",
                    Description = "Dutch techno producer and live act famous for marathon sets.",
                    Image = new byte[] { 7, 8, 9 }
                },
                Rave = new Rave
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
                StartTime = new DateTime(2025, 7, 16, 2, 15, 0),
                EndTime = new DateTime(2025, 7, 16, 5, 0, 0),
                Stage = "Mainstage"
            }
        };
        public void AddLineUp(LineUp lineUp)
        {
            LineUps.Add(lineUp);
        }

        public List<LineUp> GetLineUpByRaveId(int raveId)
        {
            throw new NotImplementedException();
        }
    }
}
