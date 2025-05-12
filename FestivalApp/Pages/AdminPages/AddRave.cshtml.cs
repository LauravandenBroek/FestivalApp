using Logic.Managers;
using Interfaces.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FestivalApp.Pages.Shared;


namespace FestivalApp.Pages.AdminPages
{
    public class AddRaveModel : AdminPageModel
    {

        private readonly RaveManager _raveManager;

        public AddRaveModel(UserManager userManager, RaveManager raveManager) : base(userManager)
        {
            _raveManager = raveManager;
        }

        [BindProperty]
        public Rave Rave { get; set; }
        public void OnGet()
        {

        }

        

        public async Task<IActionResult> OnPostAsync(IFormFile UploadedImage)
        {
            using var memoryStream = new MemoryStream();
            await UploadedImage.CopyToAsync(memoryStream);
            Rave.Image = memoryStream.ToArray();


            _raveManager.AddRave(Rave);

            return RedirectToPage("AdminRave");
        }
    }
}
