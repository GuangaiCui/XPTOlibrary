using System.ComponentModel.DataAnnotations;

namespace XPTOlibrary.Models
{
    public class Publisher
    {
        [Key]
        public int PublisherId { get; set; }
        [Required]
        public string PublisherName { get; set; }
    }
}
