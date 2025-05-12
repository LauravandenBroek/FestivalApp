using Logic.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Interfaces.Models;
using FestivalApp.Pages.Shared;

namespace FestivalApp.Pages.AdminPages
{
    public class AdminRaveModel : AdminPageModel
    {

        private readonly RaveManager _raveManager;
        public AdminRaveModel(UserManager userManager, RaveManager raveManager) : base(userManager)
        {
            _raveManager = raveManager;
        }
        public List<Rave> Raves { get; set; }
        public void OnGet()
        {
            Raves = _raveManager.GetRaves();
        }

        public IActionResult OnPostDelete(int id)
        {
            _raveManager.DeleteRave(id);
            return RedirectToPage();
        }
    }
}
