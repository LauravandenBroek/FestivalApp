using TestFestivalApp.RepositoryMocks;
using Logic.Managers;
using Logic.ViewModels;
using Logic.Exceptions;
   
namespace TestFestivalApp.UnitTests
{
    public class UserManagerTests
    {

        [Fact]
        public void RegisterUser_ReturnsRegisteredUser()
        {
            //Arrange
            var repository = new UserRepositoryMock();
            var manager = new UserManager(repository);
            int initialUsers = repository.Users.Count();

            var NewUser = new RegisterViewModel
            {
                Name = "Test",
                Email = "Test@test.nl",
                Birthdate = new DateTime(2000, 1, 5),
                Password = "Geheim123",
                ConfirmPassword = "Geheim123"
            };

            //Act & Assert
            manager.RegisterUser(NewUser);

            //Assert
            Assert.Equal(initialUsers + 1, repository.Users.Count());
        }

        [Fact]
        public void RegisterUser_ThrowsValidationException_WhenEmailAlreadyRegistred()
        {
            //Arrange
            var repository = new UserRepositoryMock();
            var manager = new UserManager(repository);

            var EmptyUser = new RegisterViewModel
            {
                Name = "alice",
                Email = "alice@example.com",
                Birthdate = new DateTime(2000, 1, 5),
                Password = "Geheim123",
                ConfirmPassword = "Geheim123"
            };

            //Act & Assert

            var ex = Assert.Throws<ValidationException>(() => manager.RegisterUser(EmptyUser));
            Assert.Equal("Email is already registered.", ex.Message);
        }

        [Fact]
        public void RegisterUser_ThrowsValidationException_WhenUserIsEmpty()
        {
            //Arrange
            var repository = new UserRepositoryMock();
            var manager = new UserManager(repository);

            var EmptyUser = new RegisterViewModel
            {
                Name = "",
                Email = "",
                Birthdate = new DateTime(2000, 1, 5),
                Password = "Geheim123",
                ConfirmPassword = "Geheim123"
            };

            //Act & Assert

            var ex = Assert.Throws<ValidationException>(() => manager.RegisterUser(EmptyUser));
            Assert.Equal("Please fill in all the fields", ex.Message);
        }


        [Fact]
        public void RegisterUser_ThrowsValidationException_WhenDateIsDefault()
        {
            //Arrange
            var repository = new UserRepositoryMock();
            var manager = new UserManager(repository);

            var EmptyUser = new RegisterViewModel
            {
                Name = "Test",
                Email = "Test@test.nl",
                Birthdate = default,
                Password = "Geheim123",
                ConfirmPassword = "Geheim123"
            };

            //Act & Assert

            var ex = Assert.Throws<ValidationException>(() => manager.RegisterUser(EmptyUser));
            Assert.Equal("Please fill in all the fields", ex.Message);
        }

        [Fact]
        public void RegisterUser_ThrowsValidationException_WhenPasswordTooShort()
        {
            //Arrange
            var repository = new UserRepositoryMock();
            var manager = new UserManager(repository);

            var EmptyUser = new RegisterViewModel
            {
                Name = "Test",
                Email = "test@test.nl",
                Birthdate = new DateTime(2000, 1, 5),
                Password = "kort1",
                ConfirmPassword = "kort1"
            };

            //Act & Assert

            var ex = Assert.Throws<ValidationException>(() => manager.RegisterUser(EmptyUser));
            Assert.Equal("Password must be at least 8 characters long.", ex.Message);
        }

        [Fact]
        public void RegisterUser_ThrowsValidationException_WhenPasswordHasNoNumber()
        {
            //Arrange
            var repository = new UserRepositoryMock();
            var manager = new UserManager(repository);

            var EmptyUser = new RegisterViewModel
            {
                Name = "Test",
                Email = "test@test.nl",
                Birthdate = new DateTime(2000, 1, 5),
                Password = "heeftgeennummer",
                ConfirmPassword = "heeftgeennummer"
            };

            //Act & Assert

            var ex = Assert.Throws<ValidationException>(() => manager.RegisterUser(EmptyUser));
            Assert.Equal("Password must contain at least one number.", ex.Message);
        }
        [Fact]
        public void RegisterUser_ThrowsValidationException_WhenPasswordsDoNotmatch()
        {
            //Arrange
            var repository = new UserRepositoryMock();
            var manager = new UserManager(repository);

            var EmptyUser = new RegisterViewModel
            {
                Name = "Test",
                Email = "test@test.nl",
                Birthdate = new DateTime(2000, 1, 5),
                Password = "Geheim123",
                ConfirmPassword = "Geheim456"
            };

            //Act & Assert

            var ex = Assert.Throws<ValidationException>(() => manager.RegisterUser(EmptyUser));
            Assert.Equal("Passwords do not match.", ex.Message);
        }

        [Fact]
        public void RegisterUser_ThrowsValidationException_WhenEmailIsAlreadyRegistered_CaseInsensitive()
        {
            var repository = new UserRepositoryMock();
            var manager = new UserManager(repository);

            var newUser = new RegisterViewModel
            {
                Name = "Test",
                Email = "ALICE@EXAMPLE.COM", 
                Birthdate = new DateTime(2000, 1, 5),
                Password = "Geheim123",
                ConfirmPassword = "Geheim123"
            };

            var ex = Assert.Throws<ValidationException>(() => manager.RegisterUser(newUser));
            Assert.Equal("Email is already registered.", ex.Message);
        }
    }
}