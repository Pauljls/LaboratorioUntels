using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UNTELSLAB.Models
{
    [Table("equipo_laboratorio")]
    public class EquipoLaboratorio
    {
        public EquipoLaboratorio()
        {
            Nombre = string.Empty;
            InformesMantenimiento = new List<InformeMantenimientoEquipo>();
            InformesCalibracion = new List<InformeCalibracion>();
            HistoricoFallas = new List<HistoricoFallasEquipo>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("nombre")]
        [Display(Name = "Nombre del Equipo")]
        [Remote(action: "VerificarNombreUnico", controller: "Equipos", ErrorMessage = "Este nombre ya está en uso")]
        [StringLength(255)]
        public string Nombre { get; set; }

        [Column("idLaboratorio")]
        public int IdLaboratorio { get; set; }

        public virtual Laboratorio? Laboratorio { get; set; }

        public virtual DatosEquipo? DatosEquipo { get; set; }

        public virtual FichaTecnicaEquipo? FichaTecnicaEquipo { get; set; }

        public virtual ICollection<InformeMantenimientoEquipo> InformesMantenimiento { get; set; }

        public virtual ICollection<InformeCalibracion> InformesCalibracion { get; set; }

        public virtual ICollection<HistoricoFallasEquipo> HistoricoFallas { get; set; }
    }
}