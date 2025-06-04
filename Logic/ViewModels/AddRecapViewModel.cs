using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Logic.ViewModels
{
    public class AddRecapViewModel
    {
        [Required(ErrorMessage = "Rave is required")]
        [StringLength(50, ErrorMessage = "Rave can be max 50 characters")]
        public string Rave {  get; set; }


        [Required(ErrorMessage = "Description is required")]
        [StringLength(1500, ErrorMessage = "Description can be max 1500 characters")]
        public string Description { get; set; }


        public List<byte[]> Album { get; set; }
    }
}
