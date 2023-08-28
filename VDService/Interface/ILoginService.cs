using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VD.Service.Result;

namespace VD.Service.Interface
{
	public interface ILoginService
	{
		public Response<LoginResult> Login(AdminLogin model);
		public Response<bool> Register(AdminRegister model);
	}
}
