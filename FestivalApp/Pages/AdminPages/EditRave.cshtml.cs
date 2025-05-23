using Logic.Managers;
using Interfaces.Models;
using Microsoft.AspNetCore.Mvc;
using FestivalApp.Pages.Shared;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FestivalApp.Pages.AdminPages
{
    public class EditRaveModel : AdminPageModel
    {

        private readonly RaveManager _raveManager;

        public EditRaveModel(UserManager userManager, RaveManager raveManager) : base(userManager)
        {
            _raveManager = raveManager;
        }

        [BindProperty]
        public Rave Rave { get; set; }


        public IActionResult OnGet(int id)
        {
            Rave = _raveManager.GetRaveById(id);

            if (Rave == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, IFormFile UploadedImage)
        {
            if (Rave == null)
            {
                return NotFound();
            }

            if (UploadedImage != null)
            {
                using var memoryStream = new MemoryStream();
                await UploadedImage.CopyToAsync(memoryStream);
                Rave.Image = memoryStream.ToArray();
            }

            else 
            {
                var existingRave = _raveManager.GetRaveById(id);
                Rave.Image = existingRave.Image;
            }

            _raveManager.UpdateRave(Rave);

            return RedirectToPage("AdminRave");
        }
    }
}
