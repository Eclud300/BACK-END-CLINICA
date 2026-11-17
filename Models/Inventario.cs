using System.ComponentModel.DataAnnotations.Schema;

namespace Clinica.Models
{
    public class Inventario
    {
        [Column("id")]
        public int Id { get; set; }
        
        [Column("clinicaid")]
        public int ClinicaId { get; set; }

        [Column("nombreproducto")]
        public string NombreProducto { get; set; } = string.Empty;

        [Column("cantidad")]
        public int Cantidad { get; set; }

        [Column("unidad")]
        public string Unidad { get; set; } = "Piezas";

        [Column("estado")]
        public string Estado { get; set; } = "Óptimo";
    }
}