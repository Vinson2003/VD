
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
		public string? Username { get; set; } = null!;
		public string? Email { get; set; }
		public string? Password { get; set; }
		public string? Passwordsalt { get; set; }
		public string? Status { get; set; } = null!;
		public long RoleId { get; set; }
	}

	public class AdminLogin
	{
		[Required]
		public long Id { get; set; }
		[Required]
		public string? Username { get; set; }
		[Required]
		public string? Password { get; set; }
	}

	public class AdminRegister
	{
		[Required]
		public string? Username { get; set; }
		[Required]
		public string? Password { get; set; }
		[Required]
		public string? ConfirmPassword { get; set; }
		public string? Email { get; set; }
		public string? Status { get; set; }
		public long RoleId { get; set; }
		public string? CreatedBy { get; set; }
	}

	public class AdminLogout
	{
		[Required]
		public long Id { get; set; }
		public string? Username { get; set; }
	}

	public class AdminAdd
	{
		[Required]
		public string? Name { get; set; }
		[Required]
		public string? Password { get; set; }
		[Required]
		public string? ConfirmPassword { get; set; }
		[Required]
		public string? Email { get; set; }
		[Required]
		public string? Status { get; set; }
		[Required]
		public long RoleId { get; set; }
		public string? RequestBy { get; set; }
	}

	public class AdminEdit
	{
		[Required]
		public long Id { get; set; }
		[Required]
		public string? Email { get; set; }
		[Required]
		public string? Status { get; set; }
		[Required]
		public long RoleId { get; set; }
		public string? RequestBy { get; set; }
	}

	public class AdminSetPassword
	{
		[Required]
		public long Id { get; set; }
		[Required]
		public string? Password { get; set; }
		[Required]
		public string? ConfirmPassword { get; set; }
		public string? RequestBy { get; set; }
	}
}
