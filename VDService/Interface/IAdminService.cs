
using VD.Service.Result;

namespace VD.Service.Interface
{
    public interface IAdminService
    {
		public HasilPaging<List<AdminData>> GetList(Paging paging, string qUsername);
		public Response<bool> Add(AdminAdd model);
		public Response<bool> Update(AdminUpdate model);
        public Response<bool> ChangePassword(AdminChangePassword req);
        public Response<bool> SetPassword(AdminSetPassword req);
    }
}
