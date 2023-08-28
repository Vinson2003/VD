using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VD.Service.Result
{
	public class PermissionList
	{
		public string Description { get; set; }
		public int Seq { get; set; }
		public int SubSeq { get; set; }
		public string Display { get; set; }
	}

	public class PermissionGetRole
	{
		public string Role { get; set; }
		public string Description { get; set; }
		public int Seq { get; set; }
		public int SubSeq { get; set; }
		public string Display { get; set; }
	}

	public class RequestPermissionUpdate
	{
		public string Username { get; set; }
		public long RoleId { get; set; }
		public List<string> Permission { get; set; }
	}

	public class PermissionResult
	{
		public string Role { get; set; }
		public string Description { get; set; }
	}
}
