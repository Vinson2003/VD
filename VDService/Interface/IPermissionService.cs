using VD.Service.Result;

namespace VD.Service.Interface
{
	public interface IPermissionService
	{
		public List<PermissionList> PermissionLists();
		public List<PermissionGetRole> RolePermission(long RoleId);
		public bool UpdatePermission(ReqPermissionUpdate req);

    }
}
