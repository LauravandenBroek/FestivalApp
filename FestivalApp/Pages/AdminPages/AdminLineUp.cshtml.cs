using Interfaces.Models;
using Logic.Managers;
using Microsoft.AspNetCore.Mvc;
using FestivalApp.Pages.Shared;
using Logic.ViewModels;
using Logic.Exceptions;

namespace FestivalApp.Pages.AdminPages
{
    public class AdminLineUpModel : AdminPageModel
    {

        private readonly LineUpManager _lineUpManager;
        private readonly RaveManager _raveManager;
        private readonly ArtistManager _artistManager;
        public AdminLineUpModel(UserManager userManager, LineUpManager lineUpManager, RaveManager raveManager, ArtistManager artistManager) : base(userManager)
        {
            _lineUpManager = lineUpManager;
            _raveManager = raveManager;
            _artistManager = artistManager;
        }


        public List<LineUp> LineUpItems { get; set; } = new List<LineUp>();
        public Rave Rave { get; set; } = new Rave();
        public List<Artist> AllArtists { get; set; } = new List<Artist>();

        [BindProperty]
        public LineUpViewModel Input { get; set; } 

        public IActionResult OnGet(int id)
        {
            try
            {
                LineUpItems = _lineUpManager.GetLineUpByRaveId(id);
                Rave = _raveManager.GetRaveById(id);
                AllArtists = _artistManager.GetArtists();

                var startTime = Rave.Date.ToDateTime(new TimeOnly(13, 0)); // 13:00
                var endTime = Rave.Date.ToDateTime(new TimeOnly(14, 0));   // 14:00

                Input = new LineUpViewModel
                {
                    StartTime = startTime,
                    EndTime = endTime
                };

                return Page();
            }
            catch (TemporaryDatabaseException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();

            }
            catch (PersistentDatabaseException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
        }



        public IActionResult OnPost(int id)
        {
            if (!ModelState.IsValid)
            {
                
                ReloadPageData(id);
                return Page();
            }

            try
            {
                var rave = _raveManager.GetRaveById(id);
                var artist = _artistManager.GetArtistById(Input.ArtistId);

                _lineUpManager.AddLineUp(Input, rave, artist);

                return RedirectToPage("/AdminPages/AdminLineUp", new { id = id });
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ReloadPageData(id);
                return Page();
            }
            catch (TemporaryDatabaseException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ReloadPageData(id);
                return Page();
            }
            catch (PersistentDatabaseException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ReloadPageData(id);
                return Page();
            }
        }


        public IActionResult OnPostDelete(int id, int raveId)
        {
            try
            {
                _lineUpManager.DeleteLineUp(id);
                return RedirectToPage("/AdminPages/AdminLineUp", new { id = raveId });
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ReloadPageData(raveId);
                return Page();
            }
            catch (TemporaryDatabaseException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ReloadPageData(raveId);
                return Page();
            }
            catch (PersistentDatabaseException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ReloadPageData(raveId);
                return Page();
            }
        }


        private void ReloadPageData(int id)
        {
            LineUpItems = _lineUpManager.GetLineUpByRaveId(id);
            Rave = _raveManager.GetRaveById(id);
            AllArtists = _artistManager.GetArtists();

            
            if (Input == null)
            {
                var startTime = Rave.Date.ToDateTime(new TimeOnly(13, 0));
                var endTime = Rave.Date.ToDateTime(new TimeOnly(14, 0));
                Input = new LineUpViewModel
                {
                    StartTime = startTime,
                    EndTime = endTime
                };
            }
        }

    }
}
