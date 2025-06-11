using Logic.Managers;
using Logic.Exceptions;
using TestFestivalApp.RepositoryMocks;

namespace TestFestivalApp.UnitTests
{
    public class FavoriteArtistManagerTests
    {

        [Fact]
        public async Task AddArtistToFavoriteArtists_ThrowsValidationExceptionWhenArtistIsAlreadyOnFavorites()
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
        public async Task AddArtistToFavoriteArtists_ThrowsValidationExceptionWhenArtistOrUserInvalid()
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
        public async Task RemoveArtistFromFavoriteArtists_ThrowsValidationExceptionWhenArtistIsNotOnFavorites()
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
        public async Task RemoveArtistFromFavoriteArtists_ThrowsValidationExceptionWhenArtistOrUserInvalid()
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
