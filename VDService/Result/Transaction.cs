using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VD.Service.Result
{
    public class TransactionData
    {
        public long Id { get; set; }
        public long BrandId { get; set; }
        public string? Brand { get; set; }
        public string? Result { get; set; }
        public DateTime? Date { get; set; }
        public string DateText { get; set; }
        public DateTime? Created { get; set; }
        public string CreatedText { get; set; }
        public string? Createdby { get; set; }
        public DateTime? Updated { get; set; }
        public string UpdatedText { get; set; }
        public string? Updatedby { get; set; }
    }

    public class TransactionAdd
    {
        [Required]
        public long BrandId { get; set; }
        [Required]
        public long Id { get; set; }
        [Required]
        public string? Result { get; set; }
        public string Date { get; set; }
        public string? RequestBy { get; set; }
    }

    public class TransactionEdit
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public long BrandId { get; set; }
        public string? Date { get; set; }
        [Required]
        public string? Result { get; set; }
        public string? RequestBy { get; set; }
    }

    public class TransactionDelete
    {
        [Required]
        public long Id { get; set; }
        public string? RequestBy { get; set; }
    }
}
