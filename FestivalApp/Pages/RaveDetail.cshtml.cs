using Logic.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Interfaces.Models;

namespace FestivalApp.Pages
{
    public class RaveDetailModel : PageModel
    {
        private readonly RaveManager _raveManager;
        private readonly LineUpManager _lineUpManager;

        public RaveDetailModel(RaveManager raveManager, LineUpManager lineUpManager) {
            _raveManager = raveManager;
            _lineUpManager = lineUpManager;
        }

        public Rave Rave { get; set; }
        public List<LineUp> LineUp { get; set; }
        public void OnGet(int id)
        {
            Rave = _raveManager.GetRaveById(id);
            LineUp = _lineUpManager.GetLineUpByRaveId(id);
        }
    }
}
