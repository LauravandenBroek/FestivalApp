using Logic.Managers;
using Microsoft.AspNetCore.Mvc;
using FestivalApp.Pages.Shared;
using Logic.ViewModels;
using Logic.Exceptions;


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
        public RaveViewModel Input { get; set; } 
       

        public async Task<IActionResult> OnPostAsync(IFormFile UploadedImage)
        {
            using var memoryStream = new MemoryStream();
            await UploadedImage.CopyToAsync(memoryStream);
            
            Input.Image = memoryStream.ToArray();

            try
            {

                _raveManager.AddRave(Input);
                return RedirectToPage("AdminRave");
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
