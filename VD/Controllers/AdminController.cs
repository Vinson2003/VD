using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VD.EF.DatatableVM;
using VD.Helper;
using VD.Models;
using VD.Service.Interface;
using VD.Service.Result;
using VD.Service.Service;

namespace VD.Controllers
{
	[Authorize]
	public class AdminController : Controller
    {
		public IAdminService AdminService = new AdminService();
		public IPermissionService PermissionService = new PermissionService();
		public IRoleService RoleService = new RoleService();

		public IActionResult Index()
        {
			var roles = RoleService.GetRoleList();
			List<SelectListItem> rolelist = new List<SelectListItem>();
			foreach (var v in roles)
			{
				rolelist.Add(new SelectListItem() { Value = v.Id.ToString(), Text = v.Role });
			}
			ViewBag.RoleList = rolelist;
			return View();
		}

		[HttpPost]
		public JsonResult Admin_Read(DatatableVM obj, string qUsername)
		{
			var col = obj.order.Select(row => row["column"]).FirstOrDefault();
			var dir = obj.order.Select(row => row["dir"]).FirstOrDefault();
			var intCol = Convert.ToInt16(col);
			var colname = obj.columns[intCol].data;

			var get = AdminService.GetList(new Paging()
			{
				Dir = dir,
				Col = colname,
				Start = obj.start,
				Length = obj.length,
			}, qUsername);

			return Json(new { draw = obj.draw, recordsFiltered = get.Total, recordsTotal = get.Total, data = get.Result });
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
        public JsonResult Add(AdminAddVM Model)
        {
            if (!string.IsNullOrEmpty(Model.Username) && !string.IsNullOrEmpty(Model.Email)
                && !string.IsNullOrEmpty(Model.Password) && Model.RoleId > 0
                && Model.Password == Model.ConfirmPassword
                )
            {
                var Send = AdminService.Add(new AdminAdd()
                {
                    Username = Model.Username,
					Password = Model.Password,
					ConfirmPassword = Model.ConfirmPassword,
					Email = Model.Email,
                    RoleId = Model.RoleId,
                    Status = Model.Status,
                    RequestBy = User.GetUsername(),
                });
				if (Send.Sts == false) { return Json(Send.Message); }
				return Json(true);
			}
			return Json(false);
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public JsonResult Update(AdminUpdateVM Model)
		{
			var Send = AdminService.Update(new AdminUpdate()
			{
				Id = Model.Id,
				Email = Model.Email,
				RoleId = Model.RoleId,
				Status = Model.Status,
				RequestBy = User.GetUsername(),
			});
			if (Send.Sts == false) { return Json(Send.Message); }
			return Json(true);
		}

        [HttpPost]
		[ValidateAntiForgeryToken]
		public JsonResult ChangePassword(ChangePasswordVM Model)
        {
            if (ModelState.IsValid)
            {
                var Send = AdminService.ChangePassword(new AdminChangePassword()
                {
                    Id = Model.Id,
                    Password = Model.OldPassword,
                    ConfirmPassword = Model.ConfirmPassword,
                    RequestBy = User.GetUsername(),
                });
                if (Send.Sts == false) { return Json(Send.Message); }
                return Json(true);
            }
            return Json(false);
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<JsonResult> SetPassword(AdminSetPasswordVM Model)
		{
			if (ModelState.IsValid)
			{
				var Send = AdminService.SetPassword(new AdminSetPassword()
				{
					Id = Model.Id,
					Password = Model.Password,
					ConfirmPassword = Model.ConfirmPassword,
					RequestBy= User.GetUsername(),
				});
				if (Send.Sts == false) { return Json(Send.Message); }
				return Json(true);
			}
			return Json(false);
		}

        public IActionResult Permission() 
		{
			var roles = RoleService.GetRoleList();
			List<SelectListItem> rolelist = new List<SelectListItem>();
			foreach (var v in roles)
			{
				rolelist.Add(new SelectListItem() { Value = v.Id.ToString(), Text = v.Role });
			}
			ViewBag.RoleList = rolelist;
			var permissionlist = PermissionService.PermissionLists();

			List<PermissionListVM> plist = new List<PermissionListVM>();
			if(permissionlist != null)
			{
				foreach (var p in permissionlist)
				{
					plist.Add(new PermissionListVM()
					{
						Seq = p.Seq,
						Description = p.Description,
						SubSeq = p.SubSeq,
						Display = p.Display,
					});
				}
			}
			return View(plist); 
		}

		[HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Permission([Bind("RoleId,Permission")] ReqPermissionUpdate model)
		{
			var send = PermissionService.UpdatePermission(new ReqPermissionUpdate()
			{
				Username = User.GetUsername(),
				RoleId = model.RoleId,
				Permission = model.Permission,
			});

			return RedirectToAction("Permission", "Admin");
		}

		[HttpGet]
		public JsonResult PermissionList(long RoleId)
		{
			var list = PermissionService.RolePermission(RoleId);
			return Json(list);
		}
	}
}
