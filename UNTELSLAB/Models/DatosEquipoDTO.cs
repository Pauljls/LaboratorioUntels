namespace UNTELSLAB.Models
{
    public class DatosEquipoDTO
    {
        public int? Id { get; set; }
        public string Procesador { get; set; }
        public string SistemaOperativo { get; set; }
        public string Memoria { get; set; }
        public IFormFile Manual { get; set; }
        public int AnoFabricacion { get; set; }
        public int idEquipo { get; set; }

    }
}