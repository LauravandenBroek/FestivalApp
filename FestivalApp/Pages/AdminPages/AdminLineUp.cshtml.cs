using Interfaces.Models;
using Logic.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FestivalApp.Pages.Shared;

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
           
            return Page();
        }

        [BindProperty] 
        public LineUp NewLineUpItem { get; set; }

        public IActionResult OnPost(int id)
        {

           // if (!ModelState.IsValid)
            //{
                // Laad bestaande LineUps en Rave opnieuw in
               // LineUpItems = _lineUpManager.GetLineUpByRaveId(id);
               /// Rave = _raveManager.GetRaveById(id);
               // AllArtists = _artistManager.GetArtists();
              //  return Page();
           // }

            // Koppel de juiste Rave aan de nieuwe LineUp
            NewLineUpItem.Rave = _raveManager.GetRaveById(id);

            _lineUpManager.AddLineUp(NewLineUpItem);

            return RedirectToPage("/AdminPages/AdminLineUp", new { id = id });
        }
    }
}
