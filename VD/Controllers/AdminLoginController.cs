using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VD.Models;
using VD.Service.Interface;
using VD.Service.Result;
using VD.Service.Service;
using VD.Helper;

namespace VD.Controllers
{
	[Authorize]
	public class AdminLoginController : Controller
	{
		private ILoginService LoginService = new LoginService();
		private IRoleService RoleService = new RoleService();

		[HttpGet]
        [AllowAnonymous]
		public IActionResult Login()
		{
			if (User.Identity.IsAuthenticated) { return RedirectToAction("Index", "Home"); }
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult>Login(LoginVM model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var send = LoginService.Login(new AdminLogin
					{
						Username = model.Username,
						Password = model.Password,
					});
					if (send.Sts == false)
					{
						ModelState.AddModelError(string.Empty, send.Message);
					}
					else if (send.Sts == true)
					{
						var claims = new List<Claim>
						{
							new Claim("Id", $"{send.Result.Id}"),
							new Claim("Username", send.Result.Username ?? ""),
							new Claim(ClaimTypes.Role, "Administrator"),
						};

						ClaimsIdentity userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
						ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
						await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties() { IsPersistent = model.RememberMe });

						return RedirectToAction("Index", "Home");
					}
				}
				catch (Exception ex)
				{
					ModelState.AddModelError(string.Empty, ex.Message);
				}
			}
			return View(model);
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		public IActionResult Register(RegisterVM model)
		{
			if (ModelState.IsValid)
			{
				var send = LoginService.Register(new AdminRegister()
				{
					Username = model.Username,
					Password = model.Password,
					ConfirmPassword = model.ConfirmPassword,
					Status = model.Status,
					Email = model.Email,
					RoleId = model.RoleId
				});
				if (send.Sts == false) { return Json(send.Message); }
				return Json(true);
			}
			return Json(false);
		}

		[HttpGet]
		[AllowAnonymous]
		public ActionResult Logout()
		{
			long userid = User.GetCustomId();
			string? Username = User.GetUsername();

			var I = new LogoutVM
			{
				Username = Username
			};
			return View(I);
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Logout(string? returnUrl = null)
		{
			// Clear the existing external cookie
			await HttpContext.SignOutAsync(
				CookieAuthenticationDefaults.AuthenticationScheme);

			return RedirectToAction("Login", "AdminLogin");
		}

	}
}
