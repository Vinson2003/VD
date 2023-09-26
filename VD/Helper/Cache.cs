﻿using Microsoft.Extensions.Caching.Memory;
using VD.Models;
using VD.Service.Interface;
using VD.Service.Result;
using VD.Service.Service;

namespace VD.Helper
{
    public class Cache
    {
        public IPermissionService PermissionService = new PermissionService();

        public static async Task<List<PermissionGetRole>> cacheperms(HttpContext HttpContext)
        {
            //var MemCache = HttpContext.RequestServices.GetService<IMemoryCache>();
            //var TranslationKey = "Perms";

            //var CacheLang = (List<PermissionResult>)MemCache.Get(TranslationKey);
            //if (CacheLang == null)
            //{
            //    var PermissionService = new PermissionService();
            //    IPermissionService PermissionService = new PermissionService();
            //    var Get = await PermissionService.Role(new RequestPermissionRole()
            //    {
            //        Username = HttpContext.User.GetUsername(),
            //        RoleId = 0
            //    });

            //    var GetLang = Cached.GetFileLanguage(lang);
            //    MemCache.Set(TranslationKey, Get.Result, new MemoryCacheEntryOptions()
            //    {
            //        SlidingExpiration = TimeSpan.FromMinutes(60)
            //    });
            //    return Get.Result;
            //}

            IPermissionService PermissionService = new PermissionService();
            var list = PermissionService.RolePermission(0);

            return list;
        }
    }
}
