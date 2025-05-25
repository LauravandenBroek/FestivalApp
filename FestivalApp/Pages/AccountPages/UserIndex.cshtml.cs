using FestivalApp.Pages.Shared;
using Interfaces.Models;
using Logic.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FestivalApp.Pages.AccountPages
{
    public class UserIndexModel : UserPageModel
    {
        private readonly AttendingRaveManager _attendingRaveManager;
        private readonly RaveWishlistManager _raveWishlistManager;
        private readonly FavoriteArtistManager _favoriteArtistManager;

        public UserIndexModel (AttendingRaveManager attendingRaveManager, RaveWishlistManager raveWishlistManager, FavoriteArtistManager favoriteArtistManager)
        {
            _attendingRaveManager = attendingRaveManager;
            _raveWishlistManager = raveWishlistManager;
            _favoriteArtistManager = favoriteArtistManager;
        }
        public List<Rave> AttendingRaves { get; set; }
        public List<Rave> Wishlist { get; set; }
        public List<Artist> FavoriteArtists {  get; set; }   
        public string Username { get; set; }

        public IActionResult OnGet()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            Username = HttpContext.Session.GetString("Username");

            if (userId == null)
            {
                return RedirectToPage("/Login");
            }

            AttendingRaves = _attendingRaveManager.Get5AttendingRavesByUserId(userId.Value);
            Wishlist = _raveWishlistManager.Get5WishlistRavesByUserId(userId.Value);
            FavoriteArtists = _favoriteArtistManager.GetFavoriteArtistsByUserId(userId.Value, 6);
            return Page();
        }
    }
}
