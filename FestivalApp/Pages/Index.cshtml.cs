using Interfaces.Models;
using FestivalApp.Pages.Shared;
using Logic.Managers;
using Logic.Exceptions;



namespace FestivalApp.Pages
{
    public class IndexModel : BasePageModel
    {

        private readonly RaveManager _raveManager;
        public List<Rave> LatestRaves { get; set; } = new List<Rave>();




        public IndexModel(RaveManager raveManager)
        {
            _raveManager = raveManager;
        }

        public void OnGet()
        {
            try
            {
                LatestRaves = _raveManager.GetUpcomingRaves(5);
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
