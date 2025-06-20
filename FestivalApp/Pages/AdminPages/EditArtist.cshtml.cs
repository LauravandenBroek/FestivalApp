using Logic.Managers;
using Interfaces.Models;
using Microsoft.AspNetCore.Mvc;
using FestivalApp.Pages.Shared;
using Logic.ViewModels;
using Logic.Exceptions;

namespace FestivalApp.Pages.AdminPages
{

    public class EditArtistModel : AdminPageModel
    {

        private readonly ArtistManager _artistManager;

        public EditArtistModel(UserManager userManager, ArtistManager artistManager) : base(userManager)
        {
            _artistManager = artistManager;
        }

        [BindProperty]
        public ArtistViewModel Input { get; set; } = new ArtistViewModel();

        public Artist Artist { get; set; } = new Artist();

        public  IActionResult OnGet(int id)
        {
            try
            {
                Artist = _artistManager.GetArtistById(id);

                if (Artist == null)
                {
                    return NotFound();
                }

                Input = new ArtistViewModel
                {
                    Id = id,
                    Name = Artist.Name,
                    Nationality = Artist.Nationality,
                    Genre = Artist.Genre,
                    Description = Artist.Description,
                    Image = Artist.Image

                };

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

        public async Task<IActionResult> OnPostAsync(int id, IFormFile UploadedImage)
        {
            try
            {
                if (UploadedImage != null)
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
                }
                else
                {
                    var existingArtist = _artistManager.GetArtistById(id);
                    Input.Image = existingArtist.Image;
                }

                _artistManager.UpdateArtist(Input);
                return RedirectToPage("AdminArtist");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError("Input.Description", ex.Message);
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
