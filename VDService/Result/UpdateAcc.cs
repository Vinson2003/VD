﻿using System.ComponentModel.DataAnnotations;

namespace VD.Service.Result
{
	public class UpdateAcc
	{
		[Required]
		public long Id { get; set; }
		[Required]
		public string Username { get; set; }
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
	}
}
