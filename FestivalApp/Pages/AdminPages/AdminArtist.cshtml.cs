using Logic.Managers;
using Interfaces.Models;
using Microsoft.AspNetCore.Mvc;
using FestivalApp.Pages.Shared;
using Logic.Exceptions;

namespace FestivalApp.Pages.AdminPages
{
    public class AdminArtistModel : AdminPageModel
    {
        private readonly ArtistManager _artistManager;

        public AdminArtistModel(UserManager userManager, ArtistManager artistManager) : base(userManager)
        {
            _artistManager = artistManager;
        }

        public List<Artist> Artists { get; set; } = new List<Artist>();

        public void OnGet()
        {
            try
            {
                Artists = _artistManager.GetArtists();
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

        public IActionResult OnPostDelete(int id)
        {
            try
            {
                _artistManager.DeleteArtist(id);
                return RedirectToPage();
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
