
using VD.Service.Result;

namespace VD.Service.Interface
{
	public interface IAccounService
	{
		public Response<LoginResult> Login(AdminLogin model);
		public Response<bool> Register(AdminRegister model);
	}
}
