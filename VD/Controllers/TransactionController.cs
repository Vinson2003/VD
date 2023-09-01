using HAOPortal.EF.DatatableVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using VD.Helper;
using VD.Models;
using VD.Service.Interface;
using VD.Service.Result;
using VD.Service.Service;

namespace VD.Controllers
{
	[Authorize]
	public class TransactionController : Controller
	{
		public ITransactionService TransactionService = new TransactionService();
		public IBrandService BrandService = new BrandService();

		public IActionResult Index()
		{
            var brands = BrandService.GetBrandList();
            List<SelectListItem> brandlist = new List<SelectListItem>();
            foreach (var v in brands)
            {
                brandlist.Add(new SelectListItem() { Value = v.Id.ToString(), Text = v.Name });
            }
            ViewBag.BrandList = brandlist;
            return View();
        }

        [HttpPost]
		public JsonResult Transaction_Read(DatatableVM obj)
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
            });

            return Json(new { draw = obj.draw, recordsFiltered = get.Total, recordsTotal = get.Total, data = get.Result });
        }

        [HttpPost]
        public JsonResult Add(TransactionAddVM Model)
        {
            //TimeSpan timeOnly = DateTime.Now.TimeOfDay; -->> Noted;
            var send = TransactionService.Add(new TransactionAdd()
            {
                //Date = Model.Date.Add(DateTime.Now.TimeOfDay),
                //Date = inCome.Date + timeOfDay,
                RequestBy = User.GetUsername(),
                BrandId = Model.BrandId,
                Result = Model.Result,
            });
            if (send.Sts == false) { return Json(send.Message); }
            return Json(true);
        }

        [HttpPost]
        public JsonResult Edit(TransactionEditVM Model)
        {
            var send = TransactionService.Edit(new TransactionEdit()
            {
                Id = Model.Id,
                RequestBy = User.GetUsername(),
                Result = Model.Result,
            });
            if (send.Sts == false) { return Json(send.Message); }
            return Json(true);
        }

        [HttpPost]
        public JsonResult Delete(TransactionDeleteVM Model)
        {
            var send = TransactionService.Delete(new TransactionDelete()
            {
                Id = Model.Id,
                RequestBy = User.GetUsername(),
            });
            if (send.Sts == false) { return Json(send.Message); }
            return Json(true);
        }

		public IActionResult Forbidden()
		{
			return View();
		}
	}
}
