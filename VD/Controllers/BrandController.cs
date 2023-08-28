using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VD.Controllers
{
	[Authorize]
	public class BrandController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
