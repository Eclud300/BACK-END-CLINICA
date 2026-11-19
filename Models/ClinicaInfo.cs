using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinica.Models
{
    [Table("Clinicas")]
    public class ClinicaInfo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Nombre { get; set; } = string.Empty;

        public string? Logo { get; set; }

        [MaxLength(255)]
        public string? Direccion { get; set; }

        [MaxLength(30)]
        public string? Telefono { get; set; }
    }
}