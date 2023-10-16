
using System.ComponentModel.DataAnnotations;

namespace VD.Service.Result
{
	public class AdminData
	{
		public long Id { get; set; }
		public string? Createdby { get; set; }
		public DateTime? Created { get; set; }
		public string? Updatedby { get; set; }
		public DateTime? Updated { get; set; }
		public string? Username { get; set; }
		public string? Email { get; set; }
		public string? Password { get; set; }
		public string? Passwordsalt { get; set; }
		public string? Status { get; set; }
		public long RoleId { get; set; }
		public string? Role { get; set; }
	}

	public class AdminLogin
	{
		public long Id { get; set; }
		public string? Username { get; set; }
		public string? Password { get; set; }
	}

	public class AdminRegister
	{
		public string? Username { get; set; }
		public string? Password { get; set; }
		public string? ConfirmPassword { get; set; }
		public string? Email { get; set; }
		public string? Status { get; set; }
		public long RoleId { get; set; }
		public string? CreatedBy { get; set; }
	}

	public class AdminLogout
	{
		public long Id { get; set; }
		public string? Username { get; set; }
	}

	public class AdminAdd
	{
		public string? Username { get; set; }
		public string? Password { get; set; }
		public string? ConfirmPassword { get; set; }
		public string? Email { get; set; }
		public string? Status { get; set; }
		public long RoleId { get; set; }
		public string? RequestBy { get; set; }
	}

	public class AdminUpdate
	{
		public long Id { get; set; }
		public string? Email { get; set; }
		public string? Status { get; set; }
		public long RoleId { get; set; }
		public string? RequestBy { get; set; }
	}

	public class AdminChangePassword
	{
        public long Id { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? RequestBy { get; set; }
    }

	public class AdminSetPassword
    {
		public long Id { get; set; }
		public string? Password { get; set; }
		public string? ConfirmPassword { get; set; }
		public string? RequestBy { get; set; }
	}

}
