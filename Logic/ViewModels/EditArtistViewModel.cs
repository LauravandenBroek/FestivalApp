using System.ComponentModel.DataAnnotations;

namespace Logic.ViewModels
{
    public class EditArtistViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(30, ErrorMessage = "Name can be max 30 characters")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Nationality is required")]
        [StringLength(20, ErrorMessage = "Nationality can be max 20 characters")]
        public string Nationality { get; set; }


        [Required(ErrorMessage = "Genre is required")]
        [StringLength(20, ErrorMessage = "Genre can be max 20 characters")]
        public string Genre { get; set; }


        [Required(ErrorMessage = "Description is required")]
        [StringLength(800, ErrorMessage = "Name can be max 800 characters")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Image is required")]
        public byte[] Image { get; set; }
    }
}
