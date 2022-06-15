using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XPTOlibrary.Models
{
    public class Cores
    {
        [Key]
        public int CoreId { get; set; }
        [Required]
        public string CoreName { get; set; }
    }
}
