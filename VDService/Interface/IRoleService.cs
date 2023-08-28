using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VD.Service.Result;

namespace VD.Service.Interface
{
	public interface IRoleService
	{
		public List<RoleList> GetRoleList(/*string Name*/);
	}
}
