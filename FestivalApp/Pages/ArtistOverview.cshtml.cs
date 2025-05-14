using Interfaces.Models;
using Logic.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FestivalApp.Pages.Shared;


namespace FestivalApp.Pages
{
    public class ArtistOverviewModel : BasePageModel
    {
        private readonly ArtistManager _artistManager;
        public List<Artist> artists { get; set; }
        public ArtistOverviewModel(ArtistManager artistManager)
        {
            _artistManager = artistManager;
        }

        public void OnGet()
        {
            artists = _artistManager.GetArtists();
        }
    }
}
