using FestivalApp.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FestivalApp.Models;

namespace FestivalApp.Pages.AdminPages
{
    public class AdminRaveModel : PageModel
    {

        private readonly RaveManager _raveManager;
        public AdminRaveModel(RaveManager raveManager)
        {
            _raveManager = raveManager;
        }
        public List<Rave> Raves { get; set; }
        public void OnGet()
        {
            //Raves = _raveManager.GetRaves();
        }
    }
}
