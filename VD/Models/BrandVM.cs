using System.ComponentModel.DataAnnotations;

namespace VD.Models
{
	public class BrandAddVM
	{
		[Required]
		public string? Name { get; set; }
		public IFormFile? BrandPicture { get; set; }
		public string? RequestBy { get; set; }
	}

	public class BrandEditVM
	{
		[Required]
		public long? Id { get; set; }
		[Required]
		public string? Name { get; set; }
		public IFormFile? BrandPicture { get; set; }
		public string? RequestBy { get; set; }
	}

	public class BrandDeleteVM
	{
		[Required]
		public long? Id { get; set; }
		public string? RequestBy { get; set; }
	}
}
