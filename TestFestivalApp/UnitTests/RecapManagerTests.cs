using TestFestivalApp.RepositoryMocks;
using Logic.Managers;
using Logic.ViewModels;
using Logic.Exceptions;

namespace TestFestivalApp.UnitTests
{
    public class RecapManagerTests
    {
        [Fact]
        public void AddRecap_ReturnsConvertedRecap()
        {
            //Arrange
            var repository = new RecapRepositoryMock();
            var manager = new RecapManager(repository);
            int initialRecaps = repository.Recaps.Count();

            var NewRecap = new RecapViewModel
            {
                Rave = "Soenda",
                Description = "Leuk",
                Album = new List<byte[]>
                {
                    new byte[] { 1, 2, 3 },
                    new byte[] { 4, 5, 6 }
                }
            };

            int UserId = 5;

            //Act & Assert
            manager.AddRecap(NewRecap, UserId);

            //Assert
            Assert.Equal(initialRecaps + 1, repository.Recaps.Count());
        }

        [Fact]
        public void AddRecap_ReturnsConvertedRecapWhenAlbumIsEmpty()
        {
            //Arrange
            var repository = new RecapRepositoryMock();
            var manager = new RecapManager(repository);
            int initialRecaps = repository.Recaps.Count();

            var EmptyRecap = new RecapViewModel
            {
                Rave = "Soenda",
                Description = "Leuk",
                Album = new List<byte[]>
                {
               
                }
            };

            int UserId = 5;

            //Act & Assert
            manager.AddRecap(EmptyRecap, UserId);

            //Assert
            Assert.Equal(initialRecaps + 1, repository.Recaps.Count());
        }

        [Fact]
        public void AddRecap_ThrowsValidationExceptionWhenRaveIsEmpty()
        {
            //Arrange
            var repository = new RecapRepositoryMock();
            var manager = new RecapManager(repository);
            int initialRecaps = repository.Recaps.Count();

            var EmptyRecap = new RecapViewModel
            {
                Rave = "",
                Description = "Leuk",
                Album = new List<byte[]>
                {
                    new byte[] { 1, 2, 3 },
                    new byte[] { 4, 5, 6 }
                }
            };

            int UserId = 5;

            //Act & Assert
          
            var ex = Assert.Throws<ValidationException>(() => manager.AddRecap(EmptyRecap, UserId));
            Assert.Equal("Please fill in all the fields.", ex.Message);
        }

        [Fact]
        public void AddRecap_ThrowsValidationExceptionWhenDescriptionIsEmpty()
        {
            //Arrange
            var repository = new RecapRepositoryMock();
            var manager = new RecapManager(repository);
            int initialRecaps = repository.Recaps.Count();

            var EmptyRecap = new RecapViewModel
            {
                Rave = "Soenda",
                Description = "",
                Album = new List<byte[]>
                {
                    new byte[] { 1, 2, 3 },
                    new byte[] { 4, 5, 6 }
                }
            };

            int UserId = 5;

            //Act & Assert
            var ex = Assert.Throws<ValidationException>(() => manager.AddRecap(EmptyRecap, UserId));
            Assert.Equal("Please fill in all the fields.", ex.Message);
        }
    }
}
