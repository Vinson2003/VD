using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VD.Service.Result
{
	public class Response<T>
	{
		public T Result { get; set; }
		public string Message { get; set; }
		public bool Sts { get; set; }
	}

	public class Paging
	{
		public int Start { get; set; }
		public int Length { get; set; }
		public string Dir { get; set; }
		public string Col { get; set; }
	}

	public class HasilPaging<T>
	{
		public int Total { get; set; }
		public T Result { get; set; }
		public string Pesan { get; set; }
	}

	public class RoleResult
	{
		public long Id { get; set; }
		public string? Name { get; set; }
	}

	public class RoleList
	{
		public long Id { get; set; }
		public string? Role { get; set; }

	}

	public class RequestRoleList
	{
		public string? Username { get; set; }
	}

	public class LoginResult
	{
		public long Id { get; set; }
		public string? Username { get; set; }
		public string? Role { get; set; }
		public long RoleId { get; set; }
	}

	public class RegisterResult
	{
		public string? Username { get; set; }
		public string? Password { get; set; }
		public string? ConfirmPassword { get; set; }
		public string? Email { get; set; }
		public string? Status { get; set; }
		public long RoleId { get; set; }
	}
}
