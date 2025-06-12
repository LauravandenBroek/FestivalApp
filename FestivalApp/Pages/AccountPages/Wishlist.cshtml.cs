using FestivalApp.Pages.Shared;
using Interfaces.Models;
using Logic.Exceptions;
using Logic.Managers;
using Microsoft.AspNetCore.Mvc;

namespace FestivalApp.Pages.AccountPages
{
    public class WishlistModel : UserPageModel
    {



        private readonly RaveWishlistManager _raveWishlistManager; 

        public WishlistModel(RaveWishlistManager WishlistManager)
        {
            _raveWishlistManager = WishlistManager;
        }

        public List<Rave> Wishlist { get; set; } = new List<Rave>();

        public IActionResult OnGet()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToPage("/Login");
            }

            try
            {
                Wishlist = _raveWishlistManager.GetRaveWishlistByUserId(userId.Value);
                return Page();
            }
            catch(TemporaryDatabaseException ex)
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
