using System.ComponentModel.DataAnnotations;

namespace Logic.ViewModels
{
    public class ArtistViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name can be max 50 characters")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Nationality is required")]
        [StringLength(40, ErrorMessage = "Nationality can be max 40 characters")]
        public string Nationality { get; set; }


        [Required(ErrorMessage = "Genre is required")]
        [StringLength(40, ErrorMessage = "Genre can be max 40 characters")]
        public string Genre { get; set; }


        [Required(ErrorMessage = "Description is required")]
        [StringLength(800, ErrorMessage = "Name can be max 800 characters")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Image is required")]
        public byte[] Image { get; set; }
    }
}
