using Logic.Managers;
using Microsoft.AspNetCore.Mvc;
using Interfaces.Models;
using FestivalApp.Pages.Shared;
using Logic.Exceptions;

namespace FestivalApp.Pages.AdminPages
{
    public class AdminRaveModel : AdminPageModel
    {

        private readonly RaveManager _raveManager;
        public AdminRaveModel(UserManager userManager, RaveManager raveManager) : base(userManager)
        {
            _raveManager = raveManager;
        }
        public List<Rave> Raves { get; set; } = new List<Rave>();
        public void OnGet()
        {
            try
            {
                Raves = _raveManager.GetRaves();
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

        public IActionResult OnPostDelete(int id)
        {
            try
            {
                _raveManager.DeleteRave(id);
                return RedirectToPage();
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
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
    }
}
