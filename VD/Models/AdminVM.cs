using System.ComponentModel.DataAnnotations;

namespace VD.Models
{
	public class LoginVM
	{
		[Required]
		[RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers allowed")]
		public string? Username { get; set; }
		[Required]
		[RegularExpression(@"^(?=.*\d)\S{8,}$", ErrorMessage = "there is a minimum of 8 characters there are numbers without spaces and emoji")]
		public string? Password { get; set; }
		public bool RememberMe { get; set; }
	}

	public class RegisterVM
	{
		[RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers allowed")]
		[Required]
		public string? Username { get; set; }
		[Required]
		[RegularExpression(@"^(?=.*\d)\S{8,}$", ErrorMessage = "there is a minimum of 8 characters there are numbers without spaces and emoji")]
		public string? Password { get; set; }
		[Required]
		[RegularExpression(@"^(?=.*\d)\S{8,}$", ErrorMessage = "there is a minimum of 8 characters there are numbers without spaces and emoji")]
		public string? ConfirmPassword { get; set; }
		[Required]
		public string? Email { get; set; }
		[Required]
		public string? Status { get; set; }
		[Required]
		public long RoleId { get; set; }
	}

	public class LogoutVM
	{
		[Required]
		public long Id { get; set; }
		public string? Username { get; set; }
	}

	public class AdminAddVM
	{
		[Required]
		public string? Username { get; set; }
		[Required]
		public string? Password { get; set; }
		[Required]
		public string? ConfirmPassword { get; set; }
		[Required]
		public string? Email { get; set; }
		public string? Status { get; set; }
		[Required]
		public long RoleId { get; set; }
		public string? RequestBy { get; set; }
	}

	public class AdminUpdateVM
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

    public class ChangePasswordVM
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? ConfirmPassword { get; set; }
        public string? RequestBy { get; set; }
    }

    public class RequestPermissionRole
    {
        public long RoleId { get; set; }
    }

    public class PermissionListVM
    {
        public string Description { get; set; } = null!;
        public int Seq { get; set; }
        public int SubSeq { get; set; }
        public string Display { get; set; } = null!;
    }
}
