using FestivalApp.Pages.Shared;
using Interfaces.Models;
using Logic.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FestivalApp.Pages.AccountPages
{
    public class WishlistModel : UserPageModel
    {



        private readonly RaveWishlistManager _raveWishlistManager; 

        public WishlistModel(RaveWishlistManager WishlistManager)
        {
            _raveWishlistManager = WishlistManager;
        }

        public List<Rave> Wishlist { get; set; }

        public IActionResult OnGet()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToPage("/Login");
            }

            Wishlist = _raveWishlistManager.GetRaveWishlistByUserId(userId.Value);
            return Page();
        }

        
    }
}
