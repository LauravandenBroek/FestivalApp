using Logic.Managers;
using Interfaces.Models;
using Microsoft.AspNetCore.Mvc;
using FestivalApp.Pages.Shared;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Logic.ViewModels;

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
        public RaveViewModel Input { get; set; }
        
        public Rave Rave { get; set; }


        public IActionResult OnGet(int id)
        {
            Rave = _raveManager.GetRaveById(id);

            if (Rave == null)
            {
                return NotFound();
            }

            Input = new RaveViewModel
            {
                Id = id,
                Name = Rave.Name, 
                Location = Rave.Location,
                Date = Rave.Date,
                Website = Rave.Website,
                Minimum_age = Rave.Minimum_age,
                Ticket_link = Rave.Ticket_link,
                Description = Rave.Description,
                Time = Rave.Time,
                Image = Rave.Image
            };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, IFormFile UploadedImage)
        {
           

            if (UploadedImage != null)
            {
                using var memoryStream = new MemoryStream();
                await UploadedImage.CopyToAsync(memoryStream);
                Input.Image = memoryStream.ToArray();
            }

            else 
            {
                var existingRave = _raveManager.GetRaveById(id);
                Input.Image = existingRave.Image;
            }

            _raveManager.UpdateRave(Input);

            return RedirectToPage("AdminRave");
        }
    }
}
