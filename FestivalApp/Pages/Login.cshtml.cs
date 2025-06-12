using Logic.Managers;
using Logic.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BCrypt.Net;
using Logic.Exceptions;

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

            try
            {
                var user = _userManager.ValidateUser(Input.Email, Input.Password);
                if (user == null)
                {
                    ModelState.AddModelError(Input.Password, "Invalid email or password");
                    return Page();
                }


                HttpContext.Session.SetString("Username", user.Name);
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetString("UserRole", user.Role);
                HttpContext.Session.SetString("UserEmail", user.Email);


                return RedirectToPage("Index");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
            catch (TemporaryDatabaseException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
            catch (PersistentDatabaseException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
        }
    }
}
