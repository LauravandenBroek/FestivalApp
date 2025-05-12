using Data;
using Interfaces.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FestivalApp.Pages.Shared;
using Logic.Managers;



namespace FestivalApp.Pages
{
    public class IndexModel : BasePageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly RaveManager _raveManager;
        public List<Rave> LatestRaves { get; set; } 



        public IndexModel(RaveManager raveManager)
        {
            _raveManager = raveManager;
        }

        public void OnGet()
        {
            LatestRaves= _raveManager.GetUpcomingRaves(5);
        }
    }
}
