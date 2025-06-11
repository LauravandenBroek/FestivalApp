using Logic.ViewModels;
using Logic.Managers;
using Logic.Exceptions;
using TestFestivalApp.RepositoryMocks;
using static System.Net.Mime.MediaTypeNames;

namespace TestFestivalApp.UnitTests
{
    public class RaveManagerTests
    {
        [Fact]
        public void AddRave_ReturnsConvertedRave()
        {
            //Arrange
            var repository = new RaveRepositoryMock();
            var manager = new RaveManager(repository);
            int initialUsers = repository.Raves.Count();

            var NewRave = new RaveViewModel
            {
               Id = 34,
               Name = "Tegendraads",
               Location = "Autotron",
               Date = new DateOnly(2025, 7, 26),
               Website = "www.tegendraads.nl",
               Minimum_age = 18,
               Ticket_link = "www.tegendraadstickets.nl",
               Description = "26 juli tegendraads zomer festival in den bosch. Haal nu je tickets!",
               Time = "11:00 - 23:00", 
               Image = [5, 4, 9]
            };

            //Act & Assert
            manager.AddRave(NewRave);

            //Assert
            Assert.Equal(initialUsers + 1, repository.Raves.Count());
        }

        [Fact]
        public void AddRave_ThrowsValidationExceptionWhenRaveIsEmpty()
        {
            //Arrange
            var repository = new RaveRepositoryMock();
            var manager = new RaveManager(repository);
           

            var EmptyRave = new RaveViewModel
            {
                Id = 34,
                Name = "",
                Location = "",
                Date = new DateOnly(2025, 7, 26),
                Website = "",
                Minimum_age = 18,
                Ticket_link = "",
                Description = "",
                Time = "",
                Image = [5, 4, 9]
            };

            //Act & Assert
            var ex = Assert.Throws<ValidationException>(() => manager.AddRave(EmptyRave));
            Assert.Equal("Please fill in all the fields.", ex.Message);
        }

        [Fact]
        public void AddRave_ThrowsValidationExceptionWhenNameTooLong()
        {
            //Arrange
            var repository = new RaveRepositoryMock();
            var manager = new RaveManager(repository);


            var EmptyRave = new RaveViewModel
            {

                Id = 34,
                Name = "Tegendraaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaads",
                Location = "Autotron",
                Date = new DateOnly(2025, 7, 26),
                Website = "www.tegendraads.nl",
                Minimum_age = 18,
                Ticket_link = "www.tegendraadstickets.nl",
                Description = "26 juli tegendraads zomer festival in den bosch. Haal nu je tickets!",
                Time = "11:00 - 23:00",
                Image = [5, 4, 9]
            };

            //Act & Assert
            var ex = Assert.Throws<ValidationException>(() => manager.AddRave(EmptyRave));
            Assert.Equal("Name can be max 50 characters.", ex.Message);
        }


        [Fact]
        public void AddRave_ThrowsValidationExceptionWhenImageIsNull()
        {
            //Arrange
            var repository = new RaveRepositoryMock();
            var manager = new RaveManager(repository);


            var EmptyImageRave = new RaveViewModel
            {

                Id = 34,
                Name = "Tegendraads",
                Location = "Autotron",
                Date = new DateOnly(2025, 7, 26),
                Website = "www.tegendraads.nl",
                Minimum_age = 18,
                Ticket_link = "www.tegendraadstickets.nl",
                Description = "26 juli tegendraads zomer festival in den bosch. Haal nu je tickets!",
                Time = "11:00 - 23:00",
                Image = []
            };

            //Act & Assert
            var ex = Assert.Throws<ValidationException>(() => manager.AddRave(EmptyImageRave));
            Assert.Equal("Please fill in all the fields.", ex.Message);
        }

    }
}
