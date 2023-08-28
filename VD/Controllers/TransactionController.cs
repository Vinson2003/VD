using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VD.Controllers
{
    
    public class TransactionController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
