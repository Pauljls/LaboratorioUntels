using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UNTELSLAB.Models
{
    [Table("informe_mantenimiento_equipo")]
    public class InformeMantenimientoEquipo
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("fecha")]
        public DateOnly Fecha { get; set; }

        [Column("lugar_mantenimiento")]
        public string? LugarMantenimiento { get; set; }

        [Column("numero_informe")]
        public string? NumeroInforme { get; set; }

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