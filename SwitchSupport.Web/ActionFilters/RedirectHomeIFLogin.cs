using Microsoft.AspNetCore.Mvc.Filters;

namespace SwitchSupport.Web.ActionFilters
{
    public class RedirectHomeIFLogin : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.HttpContext.Response.Redirect("/");
            }


        }
    }
}
