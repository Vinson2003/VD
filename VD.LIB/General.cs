using Microsoft.AspNetCore.Mvc.Rendering;

namespace VD.LIB
{
	public static class General
	{
		public static List<SelectListItem> AdminStatus()
		{
			return new List<SelectListItem>
			{
				new SelectListItem() { Text= "", Value = Consts.ADMIN_STATUS_ENABLED },
				new SelectListItem() { Text= "", Value = Consts.ADMIN_STATUS_DISABLED }
			};
		}
	}
}
