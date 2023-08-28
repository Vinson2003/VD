using VD.Service.Result;

namespace VD.Service.Interface
{
	public interface IPermissionService
	{
		public List<PermissionList> GetPermissionLists();
		public List<PermissionGetRole> GetRolePermission(long RoleId);
		public bool UpdatePermission(RequestPermissionUpdate req);
	}
}
