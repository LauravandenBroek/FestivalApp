using System.ComponentModel.DataAnnotations;

namespace Logic.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Name is required")]
        [StringLength(30, ErrorMessage = "Name can be max 30 characters")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        [StringLength(50, ErrorMessage = "Email can be max 50 characters")]
        public string Email { get; set; } 


        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "Password can be max 50 characters")]
        public string Password { get; set; }



        [Required(ErrorMessage = "Please confirm your password")] 
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Passwords don't match")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "Birthdate is required")]
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }
    }
}
