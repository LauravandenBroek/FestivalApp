using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Logic.Managers;

namespace FestivalApp.Pages.Shared
{
    public class UserPageModel : BasePageModel
    {

        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            if (IsLoggedIn() == false)
            {
                context.Result = new RedirectToPageResult("/Login");
                return;
            }

            base.OnPageHandlerExecuting(context);
        }
    }
}
