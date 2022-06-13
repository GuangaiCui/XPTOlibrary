using System.ComponentModel.DataAnnotations;

namespace XPTOlibrary.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }
        [Required]
        public string AuthorName { get; set; }
    }
}
