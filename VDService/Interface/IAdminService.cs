using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VD.Service.Result;

namespace VD.Service.Interface
{
    public interface IAdminService
    {
		public HasilPaging<List<AdminData>> GetList(Paging paging, string Username, string Password);
		public Response<bool> Add(AdminAdd model);
		public Response<bool> Edit(AdminEdit model);
		public Response<bool> ChangePassword(AdminSetPassword model, string OldPassword);
		public Response<bool> SetPassword(AdminSetPassword model);
	}
}
