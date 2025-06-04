using Interfaces.Models;
using Logic.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FestivalApp.Pages.Shared;
using Logic.ViewModels;

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


        public List<LineUp> LineUpItems { get; set; }
        public Rave Rave { get; set; }
        public List<Artist> AllArtists { get; set; }

        public IActionResult OnGet(int id)
        {
            LineUpItems = _lineUpManager.GetLineUpByRaveId(id);
            Rave = _raveManager.GetRaveById(id);
            AllArtists = _artistManager.GetArtists();

            var startTime = Rave.Date.ToDateTime(new TimeOnly(13, 0)); // 13:00
            var endTime = Rave.Date.ToDateTime(new TimeOnly(14, 0));   // 14:00

            Input = new AddLineUpViewModel
            {
                StartTime = startTime,
                EndTime = endTime
            };

            return Page();
        }

        [BindProperty] 
        public AddLineUpViewModel Input { get; set; }

        public IActionResult OnPost(int id)
        {
            var rave = _raveManager.GetRaveById(id);
            var artist = _artistManager.GetArtistById(Input.ArtistId);

            _lineUpManager.AddLineUp(Input, rave, artist);

            return RedirectToPage("/AdminPages/AdminLineUp", new { id = id });
        }
    }
}
