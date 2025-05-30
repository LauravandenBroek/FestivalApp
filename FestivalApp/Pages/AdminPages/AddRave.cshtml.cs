using Logic.Managers;
using Interfaces.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FestivalApp.Pages.Shared;
using Logic.ViewModels;


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
        public AddRaveViewModel Input { get; set; }
       
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(IFormFile UploadedImage)
        {
            using var memoryStream = new MemoryStream();
            await UploadedImage.CopyToAsync(memoryStream);
            
            Input.Image = memoryStream.ToArray();
            
            
            _raveManager.AddRave(Input);

            return RedirectToPage("AdminRave");
        }
    }
}
