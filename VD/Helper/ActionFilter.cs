using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace VD.Helper
{
	public class ActionFilter : IAsyncActionFilter
	{
		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var controller = context.Controller as Controller;

			var PermsKey = await Cache.CachePerms(context.HttpContext);
			controller.ViewBag.PermsKey = PermsKey;

			Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor actiondesc = (Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor;
			string actionname = actiondesc.ActionName.ToLower();
			string controllername = actiondesc.ControllerName.ToLower();
			string requiredpermission = String.Format("{0}-{1}", controllername, actionname);

			var notincludepermission = new List<string>(){
				"home-index", "admin-permission", "admin-index",
			};
			var notincludecontroller = new List<string>(){
				"account", "home", "admin", 
			};

			if (!notincludecontroller.Contains(controllername) && !notincludepermission.Contains(requiredpermission) &&
			   CheckPerms.CheckPermis(context.HttpContext.User.GetRole(), PermsKey, requiredpermission) == false)
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
