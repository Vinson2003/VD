using Microsoft.Extensions.Caching.Memory;
using VD.Models;
using VD.Service.Interface;
using VD.Service.Result;
using VD.Service.Service;

namespace VD.Helper
{
    public class Cached
    {
        public static async Task<List<PermissionGetRole>>cacheperms(HttpContext HttpContext)
        {
            var MemCache = HttpContext.RequestServices.GetService<IMemoryCache>();
            var TranslationKey = "Perms";

            var Cache = (List<PermissionGetRole>)MemCache.Get(TranslationKey);
            if (Cache == null)
            {
                //var PermissionService = new PermissionService();
                IPermissionService PermissionService = new PermissionService();
                var Get = PermissionService.RolePermission(0);

                MemCache.Set(TranslationKey, Get, new MemoryCacheEntryOptions()
                {
                    SlidingExpiration = TimeSpan.FromMinutes(60)
                });
                return Get;
            }

            //IPermissionService PermissionService = new PermissionService();
            //var list = PermissionService.RolePermission(0);

            return Cache;
        }
    }
}
