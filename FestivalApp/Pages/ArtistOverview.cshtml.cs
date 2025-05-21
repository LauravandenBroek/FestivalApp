using Interfaces.Models;
using Logic.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FestivalApp.Pages.Shared;
using System.Diagnostics;


namespace FestivalApp.Pages
{
    public class ArtistOverviewModel : BasePageModel
    {
        private readonly ArtistManager _artistManager;
        public List<Artist> artists { get; set; }
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
            TotalPages = (int)Math.Ceiling(_artistManager.GetTotalArtistCount() / (double)PageSize);

            var stopwatch = Stopwatch.StartNew();

            artists = _artistManager.GetArtistsPaged(Page, PageSize);

            stopwatch.Stop();
            Console.WriteLine($"Artitst query duurde: {stopwatch.ElapsedMilliseconds} ms");
            //artists = _artistManager.GetArtists();
        }
    }
}
