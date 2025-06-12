using Interfaces.Models;
using Logic.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FestivalApp.Pages.Shared;
using System.Diagnostics;
using Logic.Exceptions;


namespace FestivalApp.Pages
{
    public class ArtistOverviewModel : BasePageModel
    {
        private readonly ArtistManager _artistManager;
        public List<Artist> artists { get; set; } = new List<Artist>();
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 15;
        [BindProperty(SupportsGet = true), FromQuery(Name = "artistPage")]
        public int Page { get; set; } = 1;
        public ArtistOverviewModel(ArtistManager artistManager)
        {
            _artistManager = artistManager;
        }

        public void OnGet()
        {
            CurrentPage = Page;

            try
            {
                TotalPages = (int)Math.Ceiling(_artistManager.GetTotalArtistCount() / (double)PageSize);
                artists = _artistManager.GetArtistsPaged(Page, PageSize);
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
    }
}
