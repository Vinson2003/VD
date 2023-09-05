using VD.Service.Result;

namespace VD.Helper
{
    public class CheckPerms
    {
        public static bool checkpermis(string roleName, List<PermissionGetRole> permis, string requiredpermis)
        {
            var get = permis.Where(x => x.Description == requiredpermis && x.Role == roleName).FirstOrDefault();
            if (get != null)
            {
                return true;
            }
            return false;
        }
    }
}
