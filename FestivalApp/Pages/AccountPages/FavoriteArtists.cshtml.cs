using Interfaces.Models;
using Logic.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;

namespace FestivalApp.Pages.AccountPages
{
    public class FavoriteArtistsModel : PageModel
    {
        private readonly FavoriteArtistManager _favoriteArtistManager;

        public FavoriteArtistsModel(FavoriteArtistManager favoriteArtistsManager)
        {
            _favoriteArtistManager = favoriteArtistsManager;
        }
        public List<Artist> FavoriteArtists { get; set; }
        public string Username { get; set; }
        public IActionResult OnGet()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            Username = HttpContext.Session.GetString("Username");
            if (userId == null)
            {
                return RedirectToPage("/Login");
            }

            FavoriteArtists = _favoriteArtistManager.GetFavoriteArtistsByUserId(userId.Value);
            return Page();
        }
    }
}
