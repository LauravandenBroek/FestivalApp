using Logic.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Interfaces.Models;

namespace FestivalApp.Pages
{
    public class RaveDetailModel : PageModel
    {
        private readonly RaveManager _raveManager;

        public RaveDetailModel(RaveManager raveManager) {
            _raveManager = raveManager;
            }
        public Rave Rave { get; set; }
        public void OnGet(int id)
        {
            Rave = _raveManager.GetRaveById(id);
        }
    }
}
