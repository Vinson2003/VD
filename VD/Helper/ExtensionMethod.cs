using System.Security.Claims;

namespace VD.Helper
{
	public static class ExtensionMethod
	{
		public static string? GetUserId(this ClaimsPrincipal principal)
		{
			if (principal == null)
				throw new ArgumentNullException(nameof(principal));

			return principal.FindFirstValue(ClaimTypes.NameIdentifier);
		}

		//public static long GetCustomId(this ClaimsPrincipal principal)
		//{
		//	long Id = 0;
		//	if (principal != null)
		//	{
		//		var getId = principal.FindFirstValue("Id");
		//		Id = Convert.ToInt64(Id);
		//	}
		//	return Id;
		//}

		public static string? GetUsername(this ClaimsPrincipal principal)
		{
			string? username = "";
			if (principal != null)
			{
				username = principal.FindFirstValue("Username");
			}
			return username;
		}

		public static long? GetRoleId(this ClaimsPrincipal principal)
		{
			long? roleId = 0;
			if (principal != null)
			{
				roleId = Convert.ToInt64(principal.FindFirstValue("RoleId"));
			}
			return roleId;
		}

		public static string? GetRole(this ClaimsPrincipal principal)
		{
			string? role = "";
			if (principal != null)
			{
				role = principal.FindFirstValue("Role");
			}
			return role;
		}
	}
}
