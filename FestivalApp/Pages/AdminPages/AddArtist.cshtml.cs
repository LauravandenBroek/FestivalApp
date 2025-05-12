using Logic.Managers;
using Interfaces.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FestivalApp.Pages.Shared;
using Microsoft.AspNetCore.Identity;


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
        public Artist Artist { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync(IFormFile UploadedImage)
        { 
                using var memoryStream = new MemoryStream();
                await UploadedImage.CopyToAsync(memoryStream);
                Artist.Image = memoryStream.ToArray();
            

            _artistManager.AddArtist(Artist);

            return RedirectToPage("AdminArtist");
        }
    }
}
