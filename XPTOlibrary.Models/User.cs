using System.ComponentModel.DataAnnotations;

namespace XPTOlibrary.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public DateTime Birthday { get; set; }
        public string status { get; set; }
    }
}
