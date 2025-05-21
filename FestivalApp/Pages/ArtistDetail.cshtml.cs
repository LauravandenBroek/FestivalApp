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

        public ArtistDetailModel (ArtistManager artistManager)
        {
            _artistManager = artistManager;
        }

        [BindProperty]
        public Artist Artist { get; set; }
        public void OnGet(int id)
        {
            Artist  = _artistManager.GetArtistById(id);
        }
    }
}
