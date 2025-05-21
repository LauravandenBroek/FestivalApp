using FestivalApp.Pages.Shared;
using Interfaces.Models;
using Logic.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FestivalApp.Pages.AccountPages
{
    public class UserIndexModel : UserPageModel
    {
        private readonly AttendingRaveManager _attendingRaveManager;
        private readonly RaveWishlistManager _raveWishlistManager;

        public UserIndexModel (AttendingRaveManager attendingRaveManager, RaveWishlistManager raveWishlistManager)
        {
            _attendingRaveManager = attendingRaveManager;
            _raveWishlistManager = raveWishlistManager;
        }
        public List<Rave> AttendingRaves { get; set; }
        public List<Rave> Wishlist { get; set; }
        public string Username { get; set; }

        public IActionResult OnGet()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            Username = HttpContext.Session.GetString("Username");

            if (userId == null)
            {
                return RedirectToPage("/Login");
            }

            AttendingRaves = _attendingRaveManager.Get5AttendingRavesByUserId(userId.Value);
            Wishlist = _raveWishlistManager.Get5WishlistRavesByUserId(userId.Value);
            return Page();
        }
    }
}
