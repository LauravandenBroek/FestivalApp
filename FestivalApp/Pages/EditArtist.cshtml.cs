using FestivalApp.Managers;
using FestivalApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;

namespace FestivalApp.Pages
{

    public class EditArtistModel : PageModel
    {

        private readonly ArtistManager _artistManager;

        public EditArtistModel(ArtistManager artistManager)
        {
            _artistManager = artistManager;
        }

        [BindProperty]
        public Artist Artist { get; set; }




        public async Task<IActionResult> OnGetAsync(int id)
        {
            Artist = _artistManager.GetArtistById(id);

            if (Artist == null)
            {
                return NotFound();
            }


            return Page();
        }


        public async Task<IActionResult> OnPostAsync(int id, IFormFile UploadedImage)
        {
           
            
            if (Artist == null)
            {
                return NotFound();
            }

            Console.WriteLine("Artist ID: " + Artist.Id);
            Console.WriteLine("Artist Name: " + Artist.Name);
            Console.WriteLine("Artist Nationality: " + Artist.Nationality);
            Console.WriteLine("Artist Genre: " + Artist.Genre);
            Console.WriteLine("Artist Description: " + Artist.Description);

            if (UploadedImage != null)
            {
                using var memoryStream = new MemoryStream();
                await UploadedImage.CopyToAsync(memoryStream);
                Artist.Image = memoryStream.ToArray();
            }

            if (UploadedImage == null)
            {
                var existingArtist = _artistManager.GetArtistById(id);
                Artist.Image = existingArtist.Image; // behoud oude afbeelding
            }





            Console.WriteLine("Updating Artist: " + Artist.Name);


            _artistManager.UpdateArtist(Artist);

            return RedirectToPage("AdminArtist");
        }
    }
}
