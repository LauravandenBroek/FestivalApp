using Interfaces.Models;
using Logic.Exceptions;
using Logic.Managers;
using Microsoft.AspNetCore.Mvc;
using FestivalApp.Pages.Shared;

namespace FestivalApp.Pages.AccountPages
{
    public class RecapsModel : UserPageModel
    {

        private readonly RecapManager _recapManager;

        public RecapsModel(RecapManager recapManager)
        {
            _recapManager = recapManager;
        }

        public List<Recap> Recaps { get; set; } = new List<Recap>();
        public string Username { get; set; }

        public IActionResult OnGet()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            Username = HttpContext.Session.GetString("Username");

            if (userId == null)
            {
                return RedirectToPage("/Login");
            }

            try
            {
                Recaps = _recapManager.GetRecapsByUserId(userId.Value);
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
