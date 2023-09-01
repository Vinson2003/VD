using Microsoft.EntityFrameworkCore;
using VD.EF.Models;
using VD.LIB;
using VD.Service.Interface;
using VD.Service.Result;

namespace VD.Service
{
	public class AccountService : IAccounService
	{
		public Response<LoginResult> Login(AdminLogin model)
		{
			var Response = new Response<LoginResult>();
			Response.Result = new LoginResult();
			using var context = new VddbContext();
			{
				var user = (from d in context.MtAdmins.Include(x => x.Role)
							where d.Username == model.Username && d.Status != Consts.ADMIN_STATUS_DISABLED
							select d).FirstOrDefault();
				if (user == null)
				{
					Response.Message = "InvalidUser";
					return Response;
				}

				var ValidPassword = (Security.CheckHMAC(user.PasswordSalt, model.Password) == user.Password);
				if (!ValidPassword)
				{
					Response.Message = "InvalidPassword";
					return Response;
				}

				Response.Sts = true;
				Response.Result = new LoginResult()
				{
					Id = user.Id,
					Username = user.Username,
					Role = user.Role.Role,
					RoleId = user.RoleId,
				};
			}
			return Response;
		}

		public Response<bool> Register(AdminRegister model)
		{
			var Response = new Response<bool>();
			using var context = new VddbContext();
			{
				var newuser = (from d in context.MtAdmins
							   where d.Username == model.Username
							   select d).FirstOrDefault();
				if (newuser != null)
				{
					Response.Message = "UsernameExists";
					return Response;
				}

				MtAdmin r = new MtAdmin();
				r.Username = model.Username;
				string key = Security.RandomString(60);
				r.Password = Security.CheckHMAC(key, model.Password);
				r.PasswordSalt = key;
				r.Email = model.Email;
				r.Status = model.Status;
				r.RoleId = model.RoleId;
				r.Updated = DateTime.UtcNow;
				r.Created = DateTime.UtcNow;
				r.CreatedBy = model.CreatedBy;

				context.MtAdmins.Add(r);
				context.SaveChanges();
				Response.Result = true;
				Response.Sts = true;
			}
			return Response;
		}

		public Response<bool> UpdateAcc(UpdateAcc model)
		{
			var Response = new Response<bool>();
			Response.Result = false;
			using var context = new VddbContext();
			{
				var entity = (from d in context.MtAdmins
							  where d.Id == model.Id
							  select d).FirstOrDefault();
				if (entity == null)
				{
					Response.Message = "InvalidData";
					return Response;
				}

				if (model.Status != Consts.ADMIN_STATUS_ENABLED && model.Status != Consts.ADMIN_STATUS_DISABLED)
				{
					Response.Message = "InvalidStatus";
					return Response;
				}

				entity.Username = model.Username;
				string Key = Security.RandomString(60);
				entity.PasswordSalt = Key;
				entity.Password = Security.CheckHMAC(Key, model.Password);
				entity.Email = model.Email;
				entity.RoleId = model.RoleId;

				context.SaveChanges();

				Response.Sts = true;
				Response.Result = true;
			}

			return Response;
		}
	}
}
