using HAOPortal.EF.DatatableVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using VD.Helper;
using VD.Models;
using VD.Service.Interface;
using VD.Service.Result;
using VD.Service.Service;

namespace VD.Controllers
{
    
    public class AdminController : Controller
    {
		public IAdminService AdminService = new AdminService();
		public IPermissionService PermissionService = new PermissionService();
		public IRoleService RoleService = new RoleService();

		private IMemoryCache _cache;
		public AdminController(IMemoryCache memoryCache)
		{
			_cache = memoryCache;
		}

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
		[AllowAnonymous]
		public JsonResult Admin_Read(DatatableVM obj, string Username, string Password)
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
			}, Username, Password);

			return Json(new { draw = obj.draw, recordsFiltered = get.Total, recordsTotal = get.Total, data = get.Result });
		}

		[HttpPost]
        [AllowAnonymous]
        public JsonResult Add(AdminAddVM Model)
        {
            if (!string.IsNullOrEmpty(Model.Username) && !string.IsNullOrEmpty(Model.Email)
                && !string.IsNullOrEmpty(Model.Password) && Model.RoleId > 0
                && Model.Password == Model.ConfirmPassword
                )
            {
                var Send = AdminService.Add(new AdminAdd()
                {
                    Name = Model.Username,
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
		[AllowAnonymous]
		public JsonResult Edit(AdminEditVM Model)
		{
			var Send = AdminService.Edit(new AdminEdit()
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
		[AllowAnonymous]
		public JsonResult SetPassword(AdminSetPasswordVM Model)
		{
			if (ModelState.IsValid)
			{
				var Send = AdminService.SetPassword(new AdminSetPassword()
				{
					Id = Model.Id,
					Password = Model.Password,
					ConfirmPassword = Model.ConfirmPassword,
					RequestBy = User.GetUsername(),
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
			var PermissionList = PermissionService.GetPermissionLists();


			List<PermissionListVM> pList = new List<PermissionListVM>();
			if (PermissionList != null)
			{
				foreach (var p in PermissionList)
				{
					pList.Add(new PermissionListVM()
					{
						Seq = p.Seq,
						SubSeq = p.SubSeq,
						Display = p.Display,
						Description = p.Description
					});
				}

			}
			return View(pList);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Permission([Bind("RoleId,Permission")] RequestPermissionUpdate model)
		{
			//User.GetUserId(), model.Role, model.Permission
			var Send = PermissionService.UpdatePermission(new RequestPermissionUpdate()
			{
				Username = User.GetUsername(),
				RoleId = model.RoleId,
				Permission = model.Permission
			});

			return RedirectToAction("Permission", "Admin");
		}

		[HttpGet]
		public JsonResult PermissionList(long RoleId)
		{
			var List = PermissionService.GetRolePermission(RoleId);
			return Json(List);
		}
	}
}
