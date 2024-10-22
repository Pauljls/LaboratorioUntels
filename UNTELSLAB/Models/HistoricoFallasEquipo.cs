using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UNTELSLAB.Models
{
    [Table("historico_fallas_equipo")]
    public class HistoricoFallasEquipo
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("fecha")]
        public DateOnly Fecha { get; set; }

        [Column("ocurrencia")]
        public string? Ocurrencia { get; set; }

        [Column("revisado_por")]
        public string? RevisadoPor { get; set; }

        [Column("observaciones")]
        public string? Observaciones { get; set; }

        [Column("idEquipo")]
        public int IdEquipo { get; set; }

        [ForeignKey("IdEquipo")]
        public virtual EquipoLaboratorio? Equipo { get; set; }
    }
}