using System.ComponentModel.DataAnnotations;

namespace Logic.ViewModels
{
    public class LineUpViewModel
    {
        [Required(ErrorMessage ="Artist is required")]
        public int ArtistId {  get; set; }


        [Required(ErrorMessage = "StartTime is required")]
        public DateTime StartTime { get; set; }


        [Required(ErrorMessage = "Endtime is required")]
        public DateTime EndTime { get; set; }


        [Required(ErrorMessage = "Stage is required")]
        [StringLength(40, ErrorMessage = "Stage can be max 40 characters")]
        public string Stage {  get; set; } 
        
    }
}
