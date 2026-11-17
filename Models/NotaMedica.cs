using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinica.Models
{
    [Table("NotasMedicas")]
    public class NotaMedica
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PacienteId { get; set; }
        [ForeignKey("PacienteId")]
        public Paciente? Paciente { get; set; }

        [Required]
        public int CitaId { get; set; }
        [ForeignKey("CitaId")]
        public Cita? Cita { get; set; }

        [Required]
        public int DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public Usuario? Doctor { get; set; }

        // Sección 3: Padecimiento Actual y Signos Vitales (S.V.)
        [Required]
        public string MotivoConsulta { get; set; } = null!;

        [MaxLength(10)]
        public string? FrecuenciaCardiaca { get; set; } // FC

        [MaxLength(10)]
        public string? FrecuenciaRespiratoria { get; set; } // FR

        [Column(TypeName = "decimal(4,2)")]
        public decimal? Temperatura { get; set; } // Temp

        [MaxLength(10)]
        public string? SaturacionOxigeno { get; set; } // SatO2

        [Column(TypeName = "decimal(5,2)")]
        public decimal? Peso { get; set; } // En kg

        [Column(TypeName = "decimal(4,2)")]
        public decimal? Talla { get; set; } // En metros

        [Column(TypeName = "decimal(5,2)")]
        public decimal? IMC { get; set; }

        // Sección 4: Exploración Física
        public string? ExploracionFisica { get; set; }

        // Sección 5: Diagnósticos y Plan
        [Required]
        public string Diagnostico { get; set; } = null!;

        [MaxLength(20)]
        public string? CodigoCIE10 { get; set; }

        [Required]
        public string PlanTerapeutico { get; set; } = null!;

        public string? RecetaMedicamentos { get; set; }

        public string? EstudiosAuxiliares { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    }
}