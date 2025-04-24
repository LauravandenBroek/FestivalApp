using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FestivalApp.Models;
using FestivalApp.ViewModels;
using FestivalApp.Managers;


namespace FestivalApp.Pages
{
    
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public RegisterViewModel Input { get; set; }

        private readonly UserManager _userManager;

        public RegisterModel(UserManager userManager)
        {
            _userManager = userManager;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (_userManager.EmailExists(Input.Email))
            {
                ModelState.AddModelError("Input.Email", "Email is already registered. Already have an account? Login page");
                return Page();
            }

            var newUser = new User
            {
                Name = Input.Name,
                Email = Input.Email,
                PasswordHash = Input.Password,
                Birthdate = Input.Birthdate,
                Role = "User"
            };

            _userManager.RegisterUser(newUser);
            return RedirectToPage("Login");
        }
    }
}
