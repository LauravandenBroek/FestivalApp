using Microsoft.AspNetCore.Mvc;
using Interfaces.Models;
using FestivalApp.Pages.Shared;
using Logic.Managers;
using Logic.Exceptions;

namespace FestivalApp.Pages.AccountPages
{
    public class AttendingRavesModel : UserPageModel
    {

        private readonly AttendingRaveManager _attendingRaveManager; 

        public AttendingRavesModel(AttendingRaveManager attendingRaveManager)
        {
            _attendingRaveManager = attendingRaveManager;
        }

        public List<Rave> AttendingRaves { get; set; } = new List<Rave>();
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
                AttendingRaves = _attendingRaveManager.GetAttendingRavesByUserId(userId.Value);
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
