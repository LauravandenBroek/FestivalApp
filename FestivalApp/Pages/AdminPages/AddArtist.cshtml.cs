using Logic.Managers;
using Logic.ViewModels;
using Microsoft.AspNetCore.Mvc;
using FestivalApp.Pages.Shared;
using Logic.Exceptions;


namespace FestivalApp.Pages.AdminPages
{
    public class AddArtistModel : AdminPageModel
    {

        private readonly ArtistManager _artistManager;

        public AddArtistModel(UserManager userManager, ArtistManager artistManager)
      : base(userManager) 
        {
            _artistManager = artistManager;
        }

        [BindProperty]
        public ArtistViewModel Input { get; set; }
     
        public async Task<IActionResult> OnPostAsync(IFormFile UploadedImage)
        {
            try
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var extension = Path.GetExtension(UploadedImage.FileName).ToLowerInvariant();

                if (!allowedExtensions.Contains(extension))
                {
                    throw new ValidationException("Only JPG, JPEG, and PNG files are allowed.");
                }

                using var memoryStream = new MemoryStream();
                await UploadedImage.CopyToAsync(memoryStream);
                Input.Image = memoryStream.ToArray();

                _artistManager.AddArtist(Input);

                return RedirectToPage("AdminArtist");
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
