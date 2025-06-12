using FestivalApp.Pages.Shared;
using Interfaces.Models;
using Logic.Exceptions;
using Logic.Managers;
using Logic.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FestivalApp.Pages.AccountPages
{
    public class EditRecapModel : UserPageModel
    {
        private readonly RecapManager _recapManager;

        public EditRecapModel(RecapManager recapManager)
        {
            _recapManager = recapManager;
        }
        [BindProperty]
        public RecapViewModel Input { get; set; } = new RecapViewModel();

        [BindProperty]
        public List<IFormFile> Photos { get; set; }
        
        public Recap Recap { get; set; } = new Recap();
      

        public IActionResult OnGet(int id)
        {
            try
            {
                Recap = _recapManager.GetRecapById(id);

                if (Recap == null)
                {
                    return NotFound();
                }

                Input = new RecapViewModel
                {
                    Id = id,
                    Rave = Recap.Rave,         
                    Description = Recap.Description,
                    Album = Recap.Album
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

        public async Task<IActionResult> OnPostAsync(int id)
        {   
            Input.Album = new List<byte[]>();
            if (Photos != null)
            {
                foreach (var file in Photos.Where(f => f != null && f.Length > 0).Take(5))
                {
                    using var memoryStream = new MemoryStream();
                    await file.CopyToAsync(memoryStream);
                    Input.Album.Add(memoryStream.ToArray());
                }
            }

            else
            {
                var existingRecap = _recapManager.GetRecapById(id);
                Input.Album = existingRecap.Album;
            }
            try
            {
                _recapManager.UpdateRecap(Input);

                return RedirectToPage("/AccountPages/RecapDetail", new { id = Input.Id });
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
