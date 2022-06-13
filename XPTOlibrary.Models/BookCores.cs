using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XPTOlibrary.Models
{
    public class BookCores
    {
        
        [Display(Name ="Book Title")]
        public int BookISBN { get; set; }
        [ForeignKey("BookISBN")]
        public BookInformation BookInformation { get; set; }
        

        [Display(Name="Core")]
        public int CoreId { get; set; }
        [ForeignKey("CoreId")]
        public Cores Cores { get; set; }

        [Required]
        public int Copies { get; set; }
    }
}
