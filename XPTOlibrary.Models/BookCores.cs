using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XPTOlibrary.Models
{
    public class BookCores
    {
        [Key]
        public int BookCoreid { get; set; }
        
        [Display(Name ="Book Title")]
        public int BookISBN { get; set; }
        [ForeignKey("BookISBN")]
        [ValidateNever]
        public BookInformation BookInformation { get; set; }
        

        [Display(Name="Core")]
        public int CoreId { get; set; }
        [ForeignKey("CoreId")]
        [ValidateNever]

        public Cores Cores { get; set; }

        [Required]
        public int Copies { get; set; }
    }
}
