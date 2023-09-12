using System.ComponentModel.DataAnnotations;

namespace VD.Models
{
    public class TransactionAddVM
    {
        [Required]
        public long BrandId { get; set; }
        [Required] 
        public string? Result { get; set; }
        [Required]
        public string? Date { get; set; }
    }

    public class TransactionEditVM
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public long BrandId { get; set; }
        [Required]
        public string? Date { get; set; }
        [Required]
        public string? Result { get; set; }
    }

    public class TransactionDeleteVM
    {
        [Required]
        public long Id { get; set; }
        public string? RequestBy { get; set; }
    }
}
