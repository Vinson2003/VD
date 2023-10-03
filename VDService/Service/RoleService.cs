using VD.EF.Data;
using VD.Service.Interface;
using VD.Service.Result;

namespace VD.Service.Service
{
	public class RoleService : IRoleService
	{
		public List<RoleList> GetRoleList(/*string Name*/)
		{
			using (var context = new VddbContext())
			{
				var m = new List<RoleList>();
				using (var ctx = new VddbContext())
				{
					var getRole = from n in ctx.MtRoles
								  select new RoleList()
								  {
									  Id = n.Id,
									  Role = n.Role,
								  };

					m = getRole.ToList();
				}
				return m;
			}
		}
	}
}
