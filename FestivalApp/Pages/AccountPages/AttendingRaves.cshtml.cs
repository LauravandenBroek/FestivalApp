using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Interfaces.Models;
using FestivalApp.Pages.Shared;
using Logic.Managers;

namespace FestivalApp.Pages.AccountPages
{
    public class AttendingRavesModel : UserPageModel
    {

        private readonly AttendingRaveManager _attendingRaveManager; 

        public AttendingRavesModel(AttendingRaveManager attendingRaveManager)
        {
            _attendingRaveManager = attendingRaveManager;
        }

        public List<Rave> AttendingRaves { get; set; }

        public IActionResult OnGet()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToPage("/Login");
            }

            AttendingRaves = _attendingRaveManager.GetAttendingRavesByUserId(userId.Value);
            return Page();
        }

    }
}
