using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinica.Models
{
    [Table("Pacientes")]
    public class Paciente
    {
        [Key]
        public int Id { get; set; }

        // Identificación del Paciente
        public string? FotoUrl { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Apellidos { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string CURP { get; set; } = null!;

        [Required]
        [MaxLength(15)]
        public string Sexo { get; set; } = null!;

        public DateTime? FechaNacimiento { get; set; } // Se usa para calcular la edad

        // Sección 1: Datos de Identificación
        [MaxLength(255)]
        public string? Direccion { get; set; }

        [MaxLength(20)]
        public string? Telefono { get; set; }

        [MaxLength(100)]
        public string? ContactoEmergencia { get; set; }

        // Sección 2: Antecedentes
        public string? AntecedentesHeredoFamiliares { get; set; }

        public string? AntecedentesPatologicos { get; set; }

        public string? Alergias { get; set; } // El dueño pidió destacarlas en rojo (Esto se hará en Angular)

        public string? AntecedentesNoPatologicos { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    }
}