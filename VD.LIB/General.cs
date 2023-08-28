using HaoPortal.LIB;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HAOPortal.LIB
{
    public static class General
    {
        public static List<SelectListItem> AdminStatus()
        {
            return new List<SelectListItem>()
            {
                new SelectListItem() { Text="Active", Value=Consts.Admin_STATUS_ACTIVE },
                new SelectListItem() { Text="Inactive", Value=Consts.Admin_STATUS_INACTIVE }
            };
        }
    }
}
