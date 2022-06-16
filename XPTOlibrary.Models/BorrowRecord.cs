using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XPTOlibrary.Models
{
    public class BorrowRecord
    {
        [Key]
        public int RecordId { get; set; }

        [Required]
        [Display(Name ="Book Title")]
        public int BookISBN { get; set; }
        [ForeignKey("BookISBN")]
        [ValidateNever]
        public BookInformation BookInformation { get; set; }
        [Required]
        [Display(Name ="Core")]
        public int CoreId { get; set; }
        [ForeignKey("CoreId")]
        [ValidateNever]

        public Cores Cores { get; set; }
        [Required]
        public DateTime DateBorrow { get; set; } = DateTime.Now;
        public DateTime DateReturn { get; set; }
    }
}
