using Logic.Managers;
using Interfaces.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using FestivalApp.Pages.Shared;
using Logic.ViewModels;

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
        public EditArtistViewModel Input { get; set; }

        public Artist Artist { get; set; }

        public  IActionResult OnGet(int id)
        {
            Artist = _artistManager.GetArtistById(id);

            if (Artist == null)
            {
                return NotFound();
            }

            Input = new EditArtistViewModel
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

        public async Task<IActionResult> OnPostAsync(int id, IFormFile UploadedImage)
        { 
            if (UploadedImage != null)
            {
                using var memoryStream = new MemoryStream();
                await UploadedImage.CopyToAsync(memoryStream);
                Input.Image = memoryStream.ToArray();
            }

            if (UploadedImage == null)
            {
                var existingArtist = _artistManager.GetArtistById(id);
                Input.Image = existingArtist.Image; 
            }

            _artistManager.UpdateArtist(Input);

            return RedirectToPage("AdminArtist");
        }
    }
}
