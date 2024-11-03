using System.ComponentModel.DataAnnotations;

namespace UNTELSLAB.Models
{
    public class ManualDTO
    {
        public int Id { get; set; }
        public int idEquipo { get; set; }
        [Required]
        public IFormFile? Manual { get; set; }
    } 
}
