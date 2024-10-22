using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UNTELSLAB.Models
{
    [Table("datos_equipo")]
    public class DatosEquipo
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("procesador")]
        public string? Procesador { get; set; }

        [Column("sistema_operativo")]
        public string? SistemaOperativo { get; set; }

        [Column("memoria")]
        public string? Memoria { get; set; }

        [Column("manual")]
        public string? Manual { get; set; }

        [Column("ano_fabricacion")]
        public int AnoFabricacion { get; set; }

        [Column("idEquipo")]
        public int IdEquipo { get; set; }

        public virtual EquipoLaboratorio? EquipoLaboratorio { get; set; }
    }
}