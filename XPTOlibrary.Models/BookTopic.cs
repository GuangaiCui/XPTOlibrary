using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XPTOlibrary.Models
{
    public class BookTopic
    {
        
        [Display(Name = "Book Title")]
        public int BookISBN { get; set; }
        [ForeignKey("BookISBN")]
        public BookInformation BookInformation { get; set; }


        [Display(Name = "Topic")]
        public int TopicId { get; set; }
        [ForeignKey("TopicId")]
        public Topic Topic { get; set; }
    }
}
