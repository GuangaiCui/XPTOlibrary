using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XPTOlibrary.Models
{
    public class BookAuthor
    {

        [Display(Name = "Book Title")]
        public int BookISBN { get; set; }
        [ForeignKey("BookISBN")]
        public BookInformation BookInformation { get; set; }


        [Display(Name = "Author")]
        public int AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public Author Author { get; set; }

    }
}
