using FestivalApp.Managers;
using FestivalApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FestivalApp.Pages.AdminPages
{
    public class AdminArtistModel : PageModel
    {
        private readonly ArtistManager _artistManager;

        public AdminArtistModel(ArtistManager artistManager)
        {
            _artistManager = artistManager;
        }

        public List<Artist> Artists { get; set; }

        public void OnGet()
        {
            Artists = _artistManager.GetArtists();
        }

        public IActionResult OnPostDelete(int id)
        {
           _artistManager.DeleteArtist(id);
           return RedirectToPage();
        }
    }
}
