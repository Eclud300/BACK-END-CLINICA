using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinica.Models
{
    [Table("Insumos")]
    public class Insumo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = null!;

        [Required]
        public int Cantidad { get; set; }

        [MaxLength(50)]
        public string? UnidadMedida { get; set; } // Ej: Cajas, Piezas, Frascos

        [MaxLength(30)]
        public string Estado { get; set; } = "Óptimo"; // Ej: Óptimo, Bajo, Agotado

        public DateTime? FechaCaducidad { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    }
}