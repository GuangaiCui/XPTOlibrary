using System.ComponentModel.DataAnnotations;

namespace XPTOlibrary.Models
{
    public class Topic
    {
        [Key]
        public int TopicId { get; set; }
        [Required]
        public string TopicName { get; set; }

    }
}
