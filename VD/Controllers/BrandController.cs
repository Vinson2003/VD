﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VD.EF.DatatableVM;
using VD.Helper;
using VD.Models;
using VD.Service.Interface;
using VD.Service.Result;
using VD.Service.Service;

namespace VD.Controllers
{
	[Authorize]
	public class BrandController : Controller
	{
		public IBrandService BrandService = new BrandService();

		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public JsonResult Profile_Read(DatatableVM obj, string Brands)
		{
			var col = obj.order.Select(row => row["column"]).FirstOrDefault();
			var dir = obj.order.Select(row => row["dir"]).FirstOrDefault();
			var intcol = Convert.ToInt16(col);
			var colname = obj.columns[intcol].data;

			var get = BrandService.GetList(new Paging()
			{
				Dir = dir,
				Col = colname,
				Start = obj.start,
				Length = obj.length,
			}, Brands);

			return Json(new { draw = obj.draw, recordsFiltered = get.Total, recordsTotal = get.Total, data = get.Result });
		}

		public string SaveBrandImage(IFormFile image)
		{
			if (image != null && image.Length > 0)
			{
				var Filename = Guid.NewGuid().ToString();
				var Fileextension = Path.GetExtension(image.FileName);
				var Filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "BrandPicture", Filename + Fileextension);

				using (var Filestream = new FileStream(Filepath, FileMode.Create))
				{
					image.CopyTo(Filestream);
				}

				return Filename + Fileextension;
			}
			return null;
		}

		[HttpPost]
		public JsonResult Create(BrandAddVM Model, IFormFile image)
		{
            var brandPicFileName = SaveBrandImage(image);
            var send = BrandService.Create(new BrandAdd()
			{
				Name = Model.Name,
				BrandPicture = brandPicFileName,
				RequestBy = User.GetUsername(),
			});
            if (send.Sts == false) { return Json(send.Message); }
			return Json(true);
		}

		[HttpPost]
		public JsonResult Update(BrandEditVM Model)
		{
			var send = BrandService.Update(new BrandEdit()
			{
				Id = Model.Id,
				Name = Model.Name,
				RequestBy = User.GetUsername(),
			});
			if (send.Sts == false) { return Json(send.Message); }
			return Json(true);
		}

        [HttpPost]
        public JsonResult Delete(BrandDeleteVM Model)
        {
			var send = BrandService.Delete(new BrandDelete()
			{
				Id = Model.Id,
                RequestBy = User.GetUsername(),
			});
            if (send.Sts == false) { return Json(send.Message); }
            return Json(true);
        }

		//[HttpPost]
		//public JsonResult Delete(BrandDeleteVM Model)
		//{
		//	var send = BrandService.Delete(new BrandDelete()
		//	{
		//		Id = Model.Id,
		//		RequestBy= User.GetUsername(),
		//	});
		//	if (send.Sts == false) { return Json(send.Message); }
		//	return Json(true);
		//}

		public IActionResult Forbidden()
		{
			return View();
		}
	}
}
