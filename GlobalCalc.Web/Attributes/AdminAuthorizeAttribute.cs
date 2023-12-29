using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GlobalCalc.Web.Attributes
{
    internal class AdminAuthorizeAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.User.Identity?.IsAuthenticated != true)
                context.Result = new RedirectResult("/Admin/Login");
        }

        public void OnActionExecuting(ActionExecutingContext context) { }
    }
}
