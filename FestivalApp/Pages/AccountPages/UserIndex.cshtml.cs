using FestivalApp.Pages.Shared;
using Interfaces.Models;
using Logic.Exceptions;
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
        private readonly RecapManager _recapManager;

        public UserIndexModel (AttendingRaveManager attendingRaveManager, RaveWishlistManager raveWishlistManager, FavoriteArtistManager favoriteArtistManager, RecapManager recapManager)
        {
            _attendingRaveManager = attendingRaveManager;
            _raveWishlistManager = raveWishlistManager;
            _favoriteArtistManager = favoriteArtistManager;
            _recapManager = recapManager;

        }
        public List<Rave> AttendingRaves { get; set; } = new List<Rave>();
        public List<Rave> Wishlist { get; set; } = new List<Rave>();
        public List<Artist> FavoriteArtists {  get; set; } = new List<Artist>();
        public List<Recap> Recaps { get; set; } = new List<Recap>();
        public string Username { get; set; }

        public IActionResult OnGet()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            Username = HttpContext.Session.GetString("Username");

            if (userId == null)
            {
                return RedirectToPage("/Login");
            }

            try
            {
                AttendingRaves = _attendingRaveManager.GetAttendingRavesByUserId(userId.Value, 5);
                Wishlist = _raveWishlistManager.GetRaveWishlistByUserId(userId.Value, 5);
                FavoriteArtists = _favoriteArtistManager.GetFavoriteArtistsByUserId(userId.Value, 6);
                Recaps = _recapManager.GetRecapsByUserId(userId.Value, 3);
                return Page();
            }
            catch (TemporaryDatabaseException ex) 
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();

            }
            catch (PersistentDatabaseException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
        }
    }
}
