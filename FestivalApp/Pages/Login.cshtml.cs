using Logic.Managers;
using Logic.ViewModels;
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

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                return RedirectToPage("/Index");
            }

            return Page();
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

            HttpContext.Session.SetString("Username", user.Name);
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("UserRole", user.Role);
            HttpContext.Session.SetString("UserEmail", user.Email);


            return RedirectToPage("Index");
        }
    }
}
