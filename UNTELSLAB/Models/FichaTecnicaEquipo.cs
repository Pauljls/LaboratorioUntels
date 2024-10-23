using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UNTELSLAB.Models
{
    [Table("ficha_tecnica_equipo")]
    public class FichaTecnicaEquipo
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("marca")]
        public string? Marca { get; set; }

        [Column("serie")]
        public string? Serie { get; set; }

        [Column("tension")]
        public string? Tension { get; set; }

        [Column("frecuencia")]
        public string? Frecuencia { get; set; }

        [Column("capacidad")]
        public string? Capacidad { get; set; }

        [Column("presion")]
        public string? Presion { get; set; }

        [Column("resolucion")]
        public string? Resolucion { get; set; }

        [Column("ano_fabricacion")]
        public DateTime? AnoFabricacion { get; set; }

        [Column("ubicacion")]
        public string? Ubicacion { get; set; }

        [Column("tipo_modelo")]
        public string? TipoModelo { get; set; }

        [Column("codigo")]
        public string? Codigo { get; set; }

        [Column("corriente")]
        public string? Corriente { get; set; }

        [Column("potencia")]
        public string? Potencia { get; set; }

        [Column("velocidad")]
        public string? Velocidad { get; set; }

        [Column("temperatura")]
        public string? Temperatura { get; set; }

        [Column("canales")]
        public string? Canales { get; set; }

        [Column("fecha_operatividad")]
        public DateTime FechaOperatividad { get; set; }

        [Column("fecha_vigencia")]
        public DateTime? FechaVigencia { get; set; }

        [Column("frecuencia_calibracion")]
        public string? FrecuenciaCalibracion { get; set; }

        [Column("instruccion_operacion")]
        public string? InstruccionOperacion { get; set; }

        [Column("idEquipo")]
        public int IdEquipo { get; set; }

        [Column("frecuencia_mantenimiento")]
        public string? FrecuenciaMantenimiento { get; set; }

        [ForeignKey("IdEquipo")]
        public virtual EquipoLaboratorio? Equipo { get; set; }
    }
}