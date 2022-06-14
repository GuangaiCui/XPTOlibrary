using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XPTOlibrary.Models
{
    public class BookInformation
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        public int BookISBN { get; set; }
        [Required]
        public string BookName { get; set; }
        [Required]
        [Display(Name = "Publisher")]
        public int PublisherId { get; set; }
        [ForeignKey("PublisherId")]
        public Publisher Publisher { get; set; }
        public string Cover { get; set; }
    }
}
