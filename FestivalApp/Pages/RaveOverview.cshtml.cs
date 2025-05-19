using Logic.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Interfaces.Models;
using FestivalApp.Pages.Shared;
using System.Diagnostics;

namespace FestivalApp.Pages

{
    public class RaveOverviewModel : BasePageModel
    {
        private readonly RaveManager _raveManager;
        public List<Rave> raves { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 15;


        [BindProperty(SupportsGet = true), FromQuery(Name = "ravePage")]
        public int Page { get; set; } = 1;

        public RaveOverviewModel(RaveManager raveManager)
        {
            _raveManager = raveManager;
        }


        public void OnGet()
        {

            CurrentPage = Page;
            TotalPages = (int)Math.Ceiling(_raveManager.GetTotalRaveCount() / (double)PageSize);

            var stopwatch = Stopwatch.StartNew();

            raves = _raveManager.GetRavesPaged(Page, PageSize);

            stopwatch.Stop();
            Console.WriteLine($"Rave query duurde: {stopwatch.ElapsedMilliseconds} ms");

        }
    }
}
