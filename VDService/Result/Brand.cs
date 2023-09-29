using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace VD.Service.Result
{
	public class BrandData
	{
		public long Id { get; set; }
		public string? Name { get; set; }
		public string? Createdby { get; set; }
		public string? CreatedText { get; set; }
		public DateTime? Created { get; set; }
		public string? Updatedby { get; set; }
		public string? UpdatedText { get; set; }
		public DateTime? Updated { get; set; }
	}

    public class BrandList
    {
        public long Id { get; set; }
        public string? Name { get; set; }
    }

    public class BrandAdd
	{
		[Required]
		public string? Name { get; set; }
		public string? BrandPicture { get; set; }
		public string? RequestBy { get; set; }
	}

	public class BrandEdit
	{
		[Required]
		public long? Id { get; set; }
		[Required]
		public string? Name { get; set; }
		public IFormFile? BrandPicture { get; set; }
        public string? RequestBy { get; set; }
	}

	public class BrandDelete
	{
		[Required]
		public long? Id { get; set; }
		public string? RequestBy { get; set; }
	}
}
