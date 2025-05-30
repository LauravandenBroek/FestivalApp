using System.ComponentModel.DataAnnotations;
using System.Globalization;


namespace Logic.ViewModels
{
    public class AddRaveViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(30, ErrorMessage = "Name can be max 30 characters")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Location is required")]
        [StringLength(30, ErrorMessage = "Location can be max 30 characters")]
        public string Location { get; set; }


        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.Date)]
        public DateOnly Date {  get; set; }


        [StringLength(30, ErrorMessage = "Website can be max 30 characters")]
        [Required(ErrorMessage = "Website is required")]
        public string Website { get; set; }


        [Range(1, 99, ErrorMessage = "Minimum age must be between 1 and 99.")]
        [Required(ErrorMessage = "Minimum age is required")]
        public int Minimum_age { get; set; }


        [StringLength(30, ErrorMessage = "Ticket link can be max 30 characters")]
        [Required(ErrorMessage = "Ticket link is required")]
        public string Ticket_link { get; set; }


        [StringLength(800, ErrorMessage = "Description can be max 800 characters")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        
        [Required(ErrorMessage = "Time is required")]
        public string Time {  get; set; }

        [Required(ErrorMessage = "Image is required")]
        public byte[] Image { get; set; }

    }
}
