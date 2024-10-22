using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UNTELSLAB.Models
{
    [Table("informe_calibracion")]
    public class InformeCalibracion
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("fecha")]
        public DateOnly Fecha { get; set; }

        [Column("lugar")]
        public string? Lugar { get; set; }

        [Column("informe_certificado")]
        public string? InformeCertificado { get; set; }

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