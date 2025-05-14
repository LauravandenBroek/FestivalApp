using Logic.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Interfaces.Models;
using FestivalApp.Pages.Shared;

namespace FestivalApp.Pages

{
    public class RaveOverviewModel : BasePageModel
    {
        private readonly RaveManager _raveManager;
        public List<Rave> raves { get; set; }
        public RaveOverviewModel(RaveManager raveManager)
        {
            _raveManager = raveManager;
        }


        public void OnGet()
        {
            raves = _raveManager.GetRaves();
        }
    }
}
