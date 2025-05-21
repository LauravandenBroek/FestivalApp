using Logic.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Interfaces.Models;
using FestivalApp.Pages.Shared;

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

        public Rave Rave { get; set; }
        public List<LineUp> LineUp { get; set; }
        public bool IsAttending { get; set; }
        public string IsAttendingText => IsAttending ? "Not attending" : "Attending";
        public bool IsOnWishlist { get; set; }
        public string IsOnWishlistText => IsOnWishlist ? "Remove from wishlist" : "Wishlist";

        public void OnGet(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            Rave = _raveManager.GetRaveById(id);

            LineUp = _lineUpManager.GetLineUpByRaveId(id);
            IsAttending = userId.HasValue && _attendingRaveManager.IsUserAttendingRave(userId.Value, id);
            IsOnWishlist = userId.HasValue && _raveWishlistManager.IsRaveOnUserWishlist(userId.Value, id);
        }
        public async Task<IActionResult> OnPostToggleAttendingAsync(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToPage("/Login");
            }

            
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

        public async Task<IActionResult> OnPostToggleWishlistAsync(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToPage("/Login");
            }


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
    }
}
