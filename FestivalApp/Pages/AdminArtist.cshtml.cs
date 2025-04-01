using FestivalApp.Managers;
using FestivalApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FestivalApp.Pages
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
            // Haal de artiesten op via de ArtistManager
            Artists = _artistManager.GetArtists();
        }
    }
}
