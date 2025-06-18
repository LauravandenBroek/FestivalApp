using Logic.Managers;
using Interfaces.Models;
using Microsoft.AspNetCore.Mvc;
using FestivalApp.Pages.Shared;
using Logic.ViewModels;
using Logic.Exceptions;
using System.Linq;

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
        public RecapViewModel Input { get; set; }
        public List<IFormFile> Photos { get; set; }
        public List<Rave> AttendingRaves { get; set; } = new List<Rave>();
        
        
        public void OnGet()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            try
            {
                AttendingRaves = _attendingRaveManager.GetAttendingRavesByUserId(userId.Value);
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
       
        public async Task<IActionResult> OnPostAsync()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToPage("Login");

            Input.Album = new List<byte[]>();
            try
            {
                if (Photos != null)
                {
                    foreach (var file in Photos.Where(f => f != null && f.Length > 0).Take(5))
                    {
                        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

                        if (!allowedExtensions.Contains(extension))
                        {
                            throw new ValidationException("Only JPG, JPEG, and PNG files are allowed.");
                        }

                        using var memoryStream = new MemoryStream();
                        await file.CopyToAsync(memoryStream);
                        Input.Album.Add(memoryStream.ToArray());
                    }
                }

            

                _recapManager.AddRecap(Input, userId.Value);
                return RedirectToPage("UserIndex");
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
