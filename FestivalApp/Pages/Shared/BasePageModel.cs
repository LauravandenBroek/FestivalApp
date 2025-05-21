using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FestivalApp.Pages.Shared
{
    public class BasePageModel : PageModel
    {
        protected bool IsLoggedIn()
        {
            return HttpContext.Session.GetInt32("UserId") != null;
        }
        //protected IActionResult RequireLogin()
        //{
        //    if (!IsLoggedIn())
        //    {
        //        return RedirectToPage("/Login");
        //    }

        //    return null; 
        //}

        protected int? GetLoggedInUserId()
        {
            return HttpContext.Session.GetInt32("UserId");
        }

        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            base.OnPageHandlerExecuting(context);
            ViewData["IsLoggedIn"] = !string.IsNullOrEmpty(HttpContext.Session.GetString("Username"));
        }


    }

}
