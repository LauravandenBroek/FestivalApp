using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Interfaces.Models;
using Logic.ViewModels;
using Logic.Managers;
using Logic.Exceptions;


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
            {
                return Page();
            }

            try
            {
                _userManager.RegisterUser(Input);
                return RedirectToPage("Login");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError("Input.Birthdate", ex.Message);
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
