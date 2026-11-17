using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinica.Models
{
    [Table("Citas")]
    public class Cita
    {
        [Key]
        public int Id { get; set; }

        // Relación con el Paciente
        [Required]
        public int PacienteId { get; set; }
        [ForeignKey("PacienteId")]
        public Paciente? Paciente { get; set; }

        // Relación con el Doctor (Usuario)
        [Required]
        public int DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public Usuario? Doctor { get; set; }

        [Required]
        public DateTime FechaHora { get; set; }

        [MaxLength(255)]
        public string? MotivoConsulta { get; set; }

        [MaxLength(20)]
        public string Estado { get; set; } = "Agendada"; // Ej: Agendada, En Espera, Completada, Cancelada

        public string? NotasRecepcion { get; set; }
    }
}