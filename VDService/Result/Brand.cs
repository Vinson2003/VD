using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VD.Service.Result
{
	public class BrandData
	{
		public long Id { get; set; }
		public string? Name { get; set; }
		public string? Createdby { get; set; }
		public DateTime? Created { get; set; }
		public string? Updatedby { get; set; }
		public DateTime? Updated { get; set; }
	}

	public class BrandAdd
	{
		public string? Name { get; set; }
		public string? RequestBy { get; set; }
	}

	public class BrandEdit
	{
		public long? Id { get; set; }
		public string? Name { get; set; }
		public string? RequestBy { get; set; }
	}

	public class BrandDelete
	{ 
		public long? Id { get; set; }
		public string? RequestBy { get; set; }
	}
}
