using FestivalApp.Managers;
using FestivalApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BCrypt.Net;

namespace FestivalApp.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public LoginViewModel Input { get; set; }

        private readonly UserManager _userManager;

        public LoginModel (UserManager usermanager)
        {
            _userManager = usermanager;
        }


        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();
            var user = _userManager.GetUserByEmail(Input.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(Input.Password, user.PasswordHash))
            {

                ModelState.AddModelError(string.Empty, "Invalid email or password");
                return Page();
            }

            return RedirectToPage("Index");
        }
    }
}
