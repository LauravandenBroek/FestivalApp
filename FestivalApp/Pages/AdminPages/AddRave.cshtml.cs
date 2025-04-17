using FestivalApp.Managers;
using FestivalApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FestivalApp.Pages.AdminPages
{
    public class AddRaveModel : PageModel
    {

        private readonly RaveManager _raveManager;

        public AddRaveModel(RaveManager raveManager)
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
