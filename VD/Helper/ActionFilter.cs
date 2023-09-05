using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace VD.Helper
{
	public class ActionFilter : IAsyncActionFilter
	{
		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var controller = context.Controller as Controller;
			var permskey = await Cache.cacheperms(context.HttpContext);
			controller.ViewBag.Perms = permskey;

			Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor actiondesc = (Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor;
			string actionname = actiondesc.ActionName.ToLower();
			string controllername = actiondesc.ControllerName.ToLower();
			string requiredpermission = String.Format("{0}-{1}", controllername, actionname);

			var notincludepermission = new List<string>()
			{
				"home-index", "account-login", "admin-permission",
			};
			var notincludecontroller = new List<string>()
			{
				"account", "home", 
			};

			if(!notincludecontroller.Contains(controllername) && !notincludepermission.Contains(requiredpermission) && 
				CheckPerms.checkpermis(context.HttpContext.User.GetRole(), permskey, requiredpermission) == false)
			{
				context.Result = new RedirectToActionResult("Forbidden", "Account", null);
			}
			else
			{
				await next();
			}
        }

		public void OnActionExecutedAsync(ActionExecutedContext context)
		{
			// do something after the action executes
		}
	}
}
