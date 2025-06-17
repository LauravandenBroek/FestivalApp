using Interfaces.Models;
using FestivalApp.Pages.Shared;
using Logic.Managers;
using Logic.Exceptions;



namespace FestivalApp.Pages
{
    public class IndexModel : BasePageModel
    {

        private readonly RaveManager _raveManager;
        private readonly ArtistManager _artistManager;
        public List<Rave> LatestRaves { get; set; } = new List<Rave>();
        public List<Rave> NewlyAddedRaves { get; set; } = new List<Rave>();
        public List<Artist> FeaturedArtists { get; set; } = new List<Artist>();


        public IndexModel(RaveManager raveManager, ArtistManager artistManager)
        {
            _raveManager = raveManager;
            _artistManager = artistManager;
        }

        public void OnGet()
        {
            try
            {
                LatestRaves = _raveManager.GetUpcomingRaves(5);
                NewlyAddedRaves = _raveManager.GetRaves(5);
                FeaturedArtists = _artistManager.GetArtists(6); 
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
