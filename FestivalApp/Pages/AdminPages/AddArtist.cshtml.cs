using FestivalApp.Managers;
using FestivalApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FestivalApp.Pages.AdminPages
{
    public class AddArtistModel : PageModel
    {

        private readonly ArtistManager _artistManager;

        public AddArtistModel(ArtistManager artistManager)
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
