using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Logic.Managers;
using Interfaces.Models;
using Logic.Exceptions;


namespace FestivalApp.Pages.Shared
{
    public class AdminPageModel : BasePageModel 
    {

        private readonly UserManager _userManager;

        public AdminPageModel(UserManager userManager)
        {
            _userManager = userManager;
        }

        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            var email = HttpContext.Session.GetString("UserEmail");

            try
            {
                if (string.IsNullOrEmpty(email) || !_userManager.IsAdmin(email))
                {
                    context.Result = new RedirectToPageResult("/Index");
                }



                base.OnPageHandlerExecuting(context);
            }
            catch (TemporaryDatabaseException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                

            }
            catch (PersistentDatabaseException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                
            }
        }
    }
}
