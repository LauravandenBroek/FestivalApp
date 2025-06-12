using Logic.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Interfaces.Models;
using FestivalApp.Pages.Shared;
using Logic.Exceptions;

namespace FestivalApp.Pages
{
    public class RaveDetailModel : BasePageModel
    {
        private readonly RaveManager _raveManager;
        private readonly LineUpManager _lineUpManager;
        private readonly AttendingRaveManager _attendingRaveManager;
        private readonly RaveWishlistManager _raveWishlistManager;

        public RaveDetailModel(RaveManager raveManager, LineUpManager lineUpManager, AttendingRaveManager attendingRaveManager, RaveWishlistManager raveWishlistManager)
        {
            _raveManager = raveManager;
            _lineUpManager = lineUpManager;
            _attendingRaveManager = attendingRaveManager;
            _raveWishlistManager = raveWishlistManager;
        }

        public Rave Rave { get; set; } = new Rave();
        public List<LineUp> LineUp { get; set; } = new List<LineUp>();
        public bool IsAttending { get; set; }
        public string IsAttendingText => IsAttending ? "Not attending" : "Attending";
        public bool IsOnWishlist { get; set; }
        public string IsOnWishlistText => IsOnWishlist ? "Remove from wishlist" : "Wishlist";

        public void OnGet(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            try
            {
                Rave = _raveManager.GetRaveById(id);
                LineUp = _lineUpManager.GetLineUpByRaveId(id);

                IsAttending = userId.HasValue && _attendingRaveManager.IsUserAttendingRave(userId.Value, id);
                IsOnWishlist = userId.HasValue && _raveWishlistManager.IsRaveOnUserWishlist(userId.Value, id);
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
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
        public async Task<IActionResult> OnPostToggleAttendingAsync(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToPage("/Login");
            }

            try
            {
                bool isAttending = _attendingRaveManager.IsUserAttendingRave(userId.Value, id);

                if (isAttending)
                {
                    await _attendingRaveManager.RemoveRaveFromAttendingList(userId.Value, id);
                }
                else
                {
                    await _attendingRaveManager.AddRaveToAttendingList(userId.Value, id);
                }

                return RedirectToPage(null, new { id });
            }


            catch (ValidationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToPage(null, new { id });
            }
            catch (TemporaryDatabaseException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToPage(null, new { id });

            }
            catch (PersistentDatabaseException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToPage(null, new { id }); 
            }
        
        }

        public async Task<IActionResult> OnPostToggleWishlistAsync(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToPage("/Login");
            }

            try
            {
                bool isOnWishlist = _raveWishlistManager.IsRaveOnUserWishlist(userId.Value, id);

                if (isOnWishlist)
                {
                    await _raveWishlistManager.RemoveRaveFromWishlist(userId.Value, id);
                }
                else
                {
                    await _raveWishlistManager.AddRaveToWishList(userId.Value, id);
                }


                return RedirectToPage(null, new { id });
            }

            catch (ValidationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToPage(null, new { id });
            }
            catch (TemporaryDatabaseException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToPage(null, new { id });

            }
            catch (PersistentDatabaseException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToPage(null, new { id });
            }
        }
    }
}
