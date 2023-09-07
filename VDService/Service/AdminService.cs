using VD.EF.Models;
using VD.LIB;
using VD.Service.Interface;
using VD.Service.Result;

namespace VD.Service.Service
{
    public class AdminService : IAdminService
    {
		public HasilPaging<List<AdminData>> GetList(Paging paging, string qUsername)
		{
			var list = new HasilPaging<List<AdminData>>();
			using (var context = new VddbContext())
			{
				var getlist = (from t in context.MtAdmins
                               where string.IsNullOrEmpty(qUsername) || t.Username.ToLower().Contains(qUsername.ToLower())
                               select new AdminData()
							   {
								   Id = t.Id,
								   Username = t.Username,
								   Password = t.Password,
								   Passwordsalt = t.PasswordSalt,
								   Status = t.Status,
								   Email = t.Email,
								   Created = t.Created,
								   Createdby = t.CreatedBy,
								   Updated = t.Updated,
								   Updatedby = t.UpdatedBy,
								   RoleId = t.RoleId,
								   Role = t.Role.Role,
							   });

				if (paging.Col.ToLower() == "username")
				{
					if (paging.Dir == "asc")
					{
						getlist = getlist.OrderBy(x => x.Username);
					}
					else
					{
						getlist = getlist.OrderByDescending(x => x.Username);
					}
				}

				if (paging.Col.ToLower() == "role")
				{
					if (paging.Dir == "asc")
					{
						getlist = getlist.OrderBy(x => x.RoleId);
					}
					else
					{
						getlist = getlist.OrderByDescending(x => x.RoleId);
					}
				}

				if (paging.Col.ToLower() == "email")
				{
					if (paging.Dir == "asc")
					{
						getlist = getlist.OrderBy(x => x.Email);
					}
					else
					{
						getlist = getlist.OrderByDescending(x => x.Email);
					}
				}

				if (paging.Col.ToLower() == "status")
				{
					if (paging.Dir == "asc")
					{
						getlist = getlist.OrderBy(x => x.Status);
					}
					else
					{
						getlist = getlist.OrderByDescending(x => x.Status);
					}
				}

                list.Total = getlist.Count();
                list.Result = getlist.Skip(paging.Start).Take(paging.Length).ToList();
			}
			return list;
		}

		public Response<bool> Add(AdminAdd model)
		{
			var Response = new Response<bool>();
			using var context = new VddbContext();
			{
				var entityUser = (from w in context.MtAdmins
								  where w.Username == model.Username
								  select w).FirstOrDefault();
				if (entityUser != null)
				{
					Response.Message = "Username already exists";
					return Response;
				}

				if (!string.IsNullOrEmpty(model.Password)){ Response.Message = "InvalidPassword"; return Response; }

				if (model.Status != Consts.ADMIN_STATUS_ENABLED && model.Status != Consts.ADMIN_STATUS_DISABLED)
				{
					Response.Message = "InvalidStatus";
					return Response;
				}

				if (!string.IsNullOrEmpty(model.Email))
				{
					var entityEmail = (from d in context.MtAdmins
									   where d.Email == model.Email
									   select d).FirstOrDefault();
					if (entityEmail != null)
					{
						Response.Message = "EmailExists";
						return Response;
					}
				}

				var entityRole = (from d in context.MtRoles
								  where d.Id == model.RoleId
								  select d).FirstOrDefault();
				if (entityRole == null)
				{
					Response.Message = "InvalidRole";
					return Response;
				}

				MtAdmin a = new MtAdmin();
				a.Username = model.Username.Trim().ToLower();
				string key = Security.RandomString(60);
				a.Password = Security.CheckHMAC(key, model.Password);
				a.PasswordSalt = key;
				a.Email = model.Email;
				a.Status = model.Status;
				a.RoleId = model.RoleId;
				a.Created = DateTime.UtcNow;
				a.CreatedBy = model.RequestBy;

				context.MtAdmins.Add(a);
				context.SaveChanges();
				Response.Result = true;
				Response.Sts = true;
			}
			return Response;
		}

		public Response<bool> Update(AdminUpdate model)
		{
			var Response = new Response<bool>();
			Response.Result = false;
			using (var context = new VddbContext())
			{
				DateTime Now = DateTime.UtcNow;
				var entity = (from d in context.MtAdmins
							  where d.Id == model.Id
							  select d).FirstOrDefault();

				if (model.Status != Consts.ADMIN_STATUS_ENABLED && model.Status != Consts.ADMIN_STATUS_DISABLED)
				{
					Response.Message = "InvalidStatus";
					return Response;
				}

				//var ValidStatus = General.AdminStatus().Where(x => x.Value == model.Status).FirstOrDefault();
				//if (ValidStatus == null){ Response.Message = "InvalidStatus"; return Response; }

				entity.Email = model.Email;
				entity.Status = model.Status;
				entity.RoleId = model.RoleId;
				entity.Updated = Now;
				entity.UpdatedBy = model.RequestBy;

				context.SaveChanges();
				Response.Sts = true;
				Response.Result = true;
			}

			return Response;
		}

        public Response<bool> ChangePassword(ChangePassword req)
        {
            var Response = new Response<bool>();
            Response.Result = false;
            using (var context = new VddbContext())
            {
                var entity = (from d in context.MtAdmins
                              where d.Id == req.Id
                              select d).FirstOrDefault();
                if (entity == null)
                {
                    Response.Message = "InvalidUser";
                    return Response;
                }

                string Key = Security.RandomString(60);
                entity.PasswordSalt = Key;
                entity.Password = Security.CheckHMAC(Key, req.Password);

                context.SaveChanges();
                Response.Sts = true;
                Response.Result = true;
            }
            return Response;
        }
    }
}
