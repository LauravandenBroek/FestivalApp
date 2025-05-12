using Logic.Managers;
using Interfaces.Models;
using Microsoft.AspNetCore.Mvc;
using FestivalApp.Pages.Shared;

namespace FestivalApp.Pages.AdminPages
{
    public class AdminArtistModel : AdminPageModel
    {
        private readonly ArtistManager _artistManager;

        public AdminArtistModel(UserManager userManager, ArtistManager artistManager) : base(userManager)
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
