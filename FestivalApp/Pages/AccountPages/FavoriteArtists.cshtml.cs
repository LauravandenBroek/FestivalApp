using Interfaces.Models;
using Logic.Exceptions;
using Logic.Managers;
using Microsoft.AspNetCore.Mvc;
using FestivalApp.Pages.Shared;

namespace FestivalApp.Pages.AccountPages
{
    public class FavoriteArtistsModel : UserPageModel
    {
        private readonly FavoriteArtistManager _favoriteArtistManager;

        public FavoriteArtistsModel(FavoriteArtistManager favoriteArtistsManager)
        {
            _favoriteArtistManager = favoriteArtistsManager;
        }
        public List<Artist> FavoriteArtists { get; set; } = new List<Artist>();
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
                FavoriteArtists = _favoriteArtistManager.GetFavoriteArtistsByUserId(userId.Value);
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
