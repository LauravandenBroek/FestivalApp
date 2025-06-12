using Logic.Managers;
using Microsoft.AspNetCore.Mvc;
using Logic.Exceptions;
using Interfaces.Models;
using FestivalApp.Pages.Shared;


namespace FestivalApp.Pages

{
    public class RaveOverviewModel : BasePageModel
    {
        private readonly RaveManager _raveManager;
        public List<Rave> raves { get; set; } = new List<Rave>();
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
            

            try
            {
                TotalPages = (int)Math.Ceiling(_raveManager.GetTotalRaveCount() / (double)PageSize);
                raves = _raveManager.GetRavesPaged(Page, PageSize);
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
