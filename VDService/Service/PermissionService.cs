using VD.EF.Data;
using VD.Service.Interface;
using VD.Service.Result;

namespace VD.Service.Service
{
	public class PermissionService : IPermissionService
	{
		public List<PermissionList> PermissionLists()
		{
			using var context = new VddbContext();
			{
				var entity = from d in context.MtPermissions
							 orderby d.Seq, d.SubSeq
							 select new PermissionList
							 {
								 Description = d.Description,
								 SubSeq = d.SubSeq,
								 Seq = d.Seq,
								 Display = d.Display,
							 };
				return entity.ToList();
			}
		}

		public List<PermissionGetRole> RolePermission(long RoleId)
		{
			using var context = new VddbContext();
			{
				var entity = from d in context.PRolePermissions
							 where RoleId == 0 || d.RoleId == RoleId
							 orderby d.Permission.Seq, d.Permission.SubSeq
							 select new PermissionGetRole
							 {
								 Role = d.Role.Role,
								 Description = d.Permission.Description,
								 Display = d.Permission.Display,
								 Seq = d.Permission.Seq,
								 SubSeq = d.Permission.SubSeq,
							 };
				return entity.ToList();
			}
		}

		public bool UpdatePermission(ReqPermissionUpdate req)
		{
			bool response = true;
			using (var context = new VddbContext())
			{
				var entityPermission = from d in context.PRolePermissions
									   where d.RoleId == req.RoleId
									   select d;

				foreach (var v in entityPermission)
				{
					context.PRolePermissions.Remove(v);
				}

				var entityPermis = from d in context.MtPermissions
								   where req.Permission.Contains(d.Description)
								   select d;

				foreach (var v in entityPermis)
				{
					var a = new PRolePermission
					{
						PermissionId = v.Id,
						RoleId = req.RoleId
					};
					context.PRolePermissions.Add(a);
				}

				context.SaveChanges();
				response = true;
			}
			return response;
		}
	}
}
