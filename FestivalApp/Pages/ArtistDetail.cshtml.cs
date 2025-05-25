using Logic.Managers;
using Interfaces.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FestivalApp.Pages.Shared;

namespace FestivalApp.Pages
{
    public class ArtistDetailModel : BasePageModel
    {
        private readonly ArtistManager _artistManager;
        private readonly FavoriteArtistManager _favoriteArtistManager;

        public ArtistDetailModel (ArtistManager artistManager, FavoriteArtistManager favoriteArtistManager)
        {
            _artistManager = artistManager;
            _favoriteArtistManager = favoriteArtistManager;
        }

       
        public Artist Artist { get; set; }
        public bool IsFavorite { get; set; }
        public string IsFavoriteText => IsFavorite ? "Remove from favorites" : "Favorite";
        public void OnGet(int id)

        {
            var userId = HttpContext.Session.GetInt32("UserId");
            Artist  = _artistManager.GetArtistById(id);

            IsFavorite = userId.HasValue && _favoriteArtistManager.IsArtistOnFavorites(userId.Value, id);
        }

        public async Task<IActionResult> OnPostToggleFavoriteAsync(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToPage("/Login");
            }

            bool isFavorite = _favoriteArtistManager.IsArtistOnFavorites(userId.Value, id);

            if (isFavorite)
            {
                await _favoriteArtistManager.RemoveArtistFromFavorites(userId.Value, id);
            }
            else
            {
                await _favoriteArtistManager.AddArtistToFavorites(userId.Value, id);
            }

            return RedirectToPage(null, new { id });
        }
    }
}
