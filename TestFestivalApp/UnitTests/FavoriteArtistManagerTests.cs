using Logic.Managers;
using Logic.Exceptions;
using TestFestivalApp.RepositoryMocks;

namespace TestFestivalApp.UnitTests
{
    public class FavoriteArtistManagerTests
    {

        [Fact]
        public async Task AddArtistToFavorites_ThrowsValidationExceptionWhenArtistIsAlreadyOnFavorites()
        {
            //Arrange
            var repository = new FavoriteArtistRepositoryMock();
            var manager = new FavoriteArtistManager(repository);


            //Act & Assert
            int UserId = 1;
            int ArtistId = 5;

            var ex = await Assert.ThrowsAsync<ValidationException>(() => manager.AddArtistToFavorites(UserId, ArtistId));
            Assert.Equal("Artist is already on favorite list", ex.Message);
        }


        [Fact]
        public async Task AddArtistToFavorites_ThrowsValidationExceptionWhenArtistOrUserInvalid()
        {
            //Arrange
            var repository = new FavoriteArtistRepositoryMock();
            var manager = new FavoriteArtistManager(repository);


            //Act & Assert
            int UserId = 0;
            int ArtistId = 5;

            var ex = await Assert.ThrowsAsync<ValidationException>(() => manager.AddArtistToFavorites(UserId, ArtistId));
            Assert.Equal("Invalid user or artist ID.", ex.Message);
        }
        [Fact]
        public async Task RemoveArtistFromFavorites_ThrowsValidationExceptionWhenArtistIsNotOnFavorites()
        {
            //Arrange
            var repository = new FavoriteArtistRepositoryMock();
            var manager = new FavoriteArtistManager(repository);


            //Act & Assert
            int UserId = 8;
            int ArtistId = 5;

            var ex = await Assert.ThrowsAsync<ValidationException>(() => manager.RemoveArtistFromFavorites(UserId, ArtistId));
            Assert.Equal("Artist is not on favorite list", ex.Message);
        }

        [Fact]
        public async Task RemoveArtistFromFavorites_ThrowsValidationExceptionWhenArtistOrUserInvalid()
        {
            //Arrange
            var repository = new FavoriteArtistRepositoryMock();
            var manager = new FavoriteArtistManager(repository);


            //Act & Assert
            int UserId = 0;
            int ArtistId = 5;

            var ex = await Assert.ThrowsAsync<ValidationException>(() => manager.RemoveArtistFromFavorites(UserId, ArtistId));
            Assert.Equal("Invalid user or artist ID.", ex.Message);
        }
    }
}
