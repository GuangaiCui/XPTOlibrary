using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
        [Display(Name ="Author")]
        public int AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        [ValidateNever]
        public Author Author { get; set; }
        [Required]
        [Display(Name ="Topic")]
        public int TopicId { get; set; }
        [ForeignKey("TopicId")]
        [ValidateNever]
        public Topic Topic { get; set; }
        [Required]
        [Display(Name = "Publisher")]
        public int PublisherId { get; set; }
        [ForeignKey("PublisherId")]
        [ValidateNever]
        public Publisher Publisher { get; set; }
        public string? Cover { get; set; }
    }
}
