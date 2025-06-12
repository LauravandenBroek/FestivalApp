using Logic.Managers;
using Interfaces.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FestivalApp.Pages.Shared;
using Logic.Exceptions;

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

       
        public Artist Artist { get; set; } = new Artist();
        public bool IsFavorite { get; set; }
        public string IsFavoriteText => IsFavorite ? "Remove from favorites" : "Favorite";
        public void OnGet(int id)

        {
            var userId = HttpContext.Session.GetInt32("UserId");

            try
            {
                Artist  = _artistManager.GetArtistById(id);

                IsFavorite = userId.HasValue && _favoriteArtistManager.IsArtistOnFavorites(userId.Value, id);
            }
            catch (TemporaryDatabaseException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

            }
            catch (PersistentDatabaseException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

            }
        }

        public async Task<IActionResult> OnPostToggleFavoriteAsync(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToPage("/Login");
            }

            try
            {

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
            catch (ValidationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToPage(null, new { id });
            }
            catch (TemporaryDatabaseException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToPage(null, new { id });

            }
            catch (PersistentDatabaseException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToPage(null, new { id });
            }
        }
    }
}
