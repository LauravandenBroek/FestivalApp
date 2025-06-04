using Logic.Managers;
using Interfaces.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FestivalApp.Pages.Shared;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Logic.ViewModels;

namespace FestivalApp.Pages.AccountPages
{
    public class AddRecapModel : UserPageModel
    {
        private readonly RecapManager _recapManager;
        private readonly AttendingRaveManager _attendingRaveManager;

        public AddRecapModel(RecapManager recapManager, AttendingRaveManager attendingRaveManager) 
        { 
            _recapManager = recapManager;
            _attendingRaveManager = attendingRaveManager;
        }

        [BindProperty]
        public AddRecapViewModel Input { get; set; }
        public List<IFormFile> Photos { get; set; }
        public List<Rave> AttendingRaves { get; set; }
        
        
        public void OnGet()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            AttendingRaves = _attendingRaveManager.GetAttendingRavesByUserId(userId.Value);
        }
       
        public async Task<IActionResult> OnPostAsync()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToPage("Login");

            Input.Album = new List<byte[]>();
            if (Photos != null)
            {
                foreach (var file in Photos.Where(f => f != null && f.Length > 0).Take(5))
                {
                    using var memoryStream = new MemoryStream();
                    await file.CopyToAsync(memoryStream);
                    Input.Album.Add(memoryStream.ToArray());
                }
            }
            
            _recapManager.AddRecap(Input, userId.Value);
            return RedirectToPage("UserIndex");
        }
    }
}
