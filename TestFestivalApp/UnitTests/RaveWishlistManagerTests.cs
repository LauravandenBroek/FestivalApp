using Logic.Exceptions;
using Logic.Managers;
using Logic.ViewModels;
using TestFestivalApp.RepositoryMocks;

namespace TestFestivalApp.UnitTests
{
    public class RaveWishlistManagerTests
    {
        [Fact]
        public async Task AddRaveToWishList_ThrowsValidationExceptionWhenRaveIsAlreadyOnWishlist()
        {
            //Arrange
            var repository = new RaveWishlistRepositoryMock();
            var manager = new RaveWishlistManager(repository);


            //Act & Assert
            int UserId = 1;
            int RaveId = 5;

            var ex = await Assert.ThrowsAsync<ValidationException>(() => manager.AddRaveToWishList(UserId, RaveId));
            Assert.Equal("Rave is already on your wishlist", ex.Message);
        }


        [Fact]
        public async Task AddRaveToWishList_ThrowsValidationExceptionWhenRaveOrUserInvalid()
        {
            //Arrange
            var repository = new RaveWishlistRepositoryMock();
            var manager = new RaveWishlistManager(repository);


            //Act & Assert
            int UserId = 0;
            int RaveId = 5;

            var ex = await Assert.ThrowsAsync<ValidationException>(() => manager.AddRaveToWishList(UserId, RaveId));
            Assert.Equal("Invalid user or rave ID.", ex.Message);
        }
        [Fact]
        public async Task RemoveRaveFromWishList_ThrowsValidationExceptionWhenRaveIsNotOnWishlist()
        {
            //Arrange
            var repository = new RaveWishlistRepositoryMock();
            var manager = new RaveWishlistManager(repository);


            //Act & Assert
            int UserId = 8;
            int RaveId = 5;

            var ex = await Assert.ThrowsAsync<ValidationException>(() => manager.RemoveRaveFromWishlist(UserId, RaveId));
            Assert.Equal("Rave is not on your Wishlist", ex.Message);
        }

        [Fact]
        public async Task RemoveRaveFromWishList_ThrowsValidationExceptionWhenRaveOrUserInvalid()
        {
            //Arrange
            var repository = new RaveWishlistRepositoryMock();
            var manager = new RaveWishlistManager(repository);


            //Act & Assert
            int UserId = 0;
            int RaveId = 5;

            var ex = await Assert.ThrowsAsync<ValidationException>(() => manager.RemoveRaveFromWishlist(UserId, RaveId));
            Assert.Equal("Invalid user or rave ID.", ex.Message);
        }
    }
}
