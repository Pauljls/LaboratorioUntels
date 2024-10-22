using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UNTELSLAB.Models
{
    [Table("laboratorio")]  // Especifica el nombre exacto de la tabla en la base de datos
    public class Laboratorio
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public string RefUbicacion { get; set; } = string.Empty;

        public bool ServicioInternet { get; set; }

        [Required]
        public int Aforo { get; set; }

        [Required]
        public string EstadoLaboratorio { get; set; } = string.Empty;
    }
}