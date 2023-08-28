using VD.Service.Result;

namespace VD.Helper
{
	public class CheckPerms
	{
		public static bool CheckPermis(string roleName, List<PermissionGetRole> Permis, string requiredPermis)
		{
			var get = Permis.Where(x => x.Description == requiredPermis && x.Role == roleName).FirstOrDefault();

			if (get != null)
			{
				return true;
			}

			return false;
		}
	}
}
