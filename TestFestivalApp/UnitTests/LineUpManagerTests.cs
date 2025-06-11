using Logic.Managers;
using Logic.ViewModels;
using Interfaces.Models;
using Logic.Exceptions;

using TestFestivalApp.RepositoryMocks;

namespace TestFestivalApp.UnitTests
{
    public class LineUpManagerTests
    {
        [Fact]
        public void AddLineUp_ReturnsConvertedRave()
        {
            //Arrange
            var repository = new LineUpRepositoryMock();
            var manager = new LineUpManager(repository);
            int initialUsers = repository.LineUps.Count();

            var LineUp = new LineUpViewModel
            {
                StartTime = new DateTime(2025, 7, 15, 22, 0, 0),
                EndTime = new DateTime(2025, 7, 16, 0, 0, 0),
                Stage = "Mainstage"
            };

            var Rave = new Rave
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
            };

            var Artist = new Artist
            {
                Id = 2,
                Name = "Amelie Lens",
                Nationality = "Belgium",
                Genre = "Techno",
                Description = "One of the most prominent DJs in the European techno scene.",
                Image = new byte[] { 4, 5, 6 }
            };

            //Act & Assert
            manager.AddLineUp(LineUp, Rave, Artist);

            //Assert
            Assert.Equal(initialUsers + 1, repository.LineUps.Count());
        }

        [Fact]
        public void AddLineUp_ThrowsValidationExceptionWhenStageIsEmpty()
        {
            //Arrange
            var repository = new LineUpRepositoryMock();
            var manager = new LineUpManager(repository);
            int initialUsers = repository.LineUps.Count();

            var LineUp = new LineUpViewModel
            {
                StartTime = new DateTime(2025, 7, 15, 22, 0, 0),
                EndTime = new DateTime(2025, 7, 16, 0, 0, 0),
                Stage = ""
            };

            var Rave = new Rave
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
            };

            var Artist = new Artist
            {
                Id = 2,
                Name = "Amelie Lens",
                Nationality = "Belgium",
                Genre = "Techno",
                Description = "One of the most prominent DJs in the European techno scene.",
                Image = new byte[] { 4, 5, 6 }
            };

            //Act & Assert
            var ex = Assert.Throws<ValidationException>(() => manager.AddLineUp(LineUp, Rave, Artist));
            Assert.Equal("Please fill in all the fields.", ex.Message);
        }

        [Fact]
        public void AddLineUp_ThrowsValidationExceptionWhenRaveIsNull()
        {
            //Arrange
            var repository = new LineUpRepositoryMock();
            var manager = new LineUpManager(repository);
            int initialUsers = repository.LineUps.Count();

            var LineUp = new LineUpViewModel
            {
                StartTime = new DateTime(2025, 7, 15, 22, 0, 0),
                EndTime = new DateTime(2025, 7, 16, 0, 0, 0),
                Stage = "Mainstage"
            };



            var Artist = new Artist
            {
                Id = 2,
                Name = "Amelie Lens",
                Nationality = "Belgium",
                Genre = "Techno",
                Description = "One of the most prominent DJs in the European techno scene.",
                Image = new byte[] { 4, 5, 6 }
            };

            //Act & Assert
            var ex = Assert.Throws<ValidationException>(() => manager.AddLineUp(LineUp, null, Artist));
            Assert.Equal("Please fill in all the fields.", ex.Message);
        }

        [Fact]
        public void AddLineUp_ThrowsValidationExceptionWhenArtistIsNull()
        {
            //Arrange
            var repository = new LineUpRepositoryMock();
            var manager = new LineUpManager(repository);
            int initialUsers = repository.LineUps.Count();

            var LineUp = new LineUpViewModel
            {
                StartTime = new DateTime(2025, 7, 15, 22, 0, 0),
                EndTime = new DateTime(2025, 7, 16, 0, 0, 0),
                Stage = "Mainstage"
            };

            var Rave = new Rave
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
            };
           

            //Act & Assert
            var ex = Assert.Throws<ValidationException>(() => manager.AddLineUp(LineUp, Rave, null));
            Assert.Equal("Please fill in all the fields.", ex.Message);
        }
    }
}
