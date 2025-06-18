using Logic.Managers;
using Interfaces.Models;
using Microsoft.AspNetCore.Mvc;
using FestivalApp.Pages.Shared;
using Logic.ViewModels;
using Logic.Exceptions;

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
        public RaveViewModel Input { get; set; } = new RaveViewModel();
        
        public Rave Rave { get; set; } = new Rave();


        public IActionResult OnGet(int id)
        {
            try
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
                    var existingRave = _raveManager.GetRaveById(id); 
                    if (existingRave == null)
                    {
                        throw new PersistentDatabaseException(); 
                    }
                    Input.Image = existingRave.Image;
                }

                _raveManager.UpdateRave(Input);

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
