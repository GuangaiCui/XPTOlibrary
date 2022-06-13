using System.ComponentModel.DataAnnotations;

namespace XPTOlibrary.Models
{
    public class City
    {
        [Key]
        public int CityId { get; set; }
        [Required]
        public string CityName { get; set; }
    }
}
