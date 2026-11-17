namespace Clinica.DTOs
{
    public class NotaMedicaRespuestaDTO
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public int CitaId { get; set; }
        public int DoctorId { get; set; }
        public string NombrePaciente { get; set; } = null!;
        public string NombreDoctor { get; set; } = null!;

        // Sección 3: Padecimiento y Signos Vitales
        public string MotivoConsulta { get; set; } = null!;
        public string? FrecuenciaCardiaca { get; set; }
        public string? FrecuenciaRespiratoria { get; set; }
        public decimal? Temperatura { get; set; }
        public string? SaturacionOxigeno { get; set; }
        public decimal? Peso { get; set; }
        public decimal? Talla { get; set; }
        public decimal? IMC { get; set; }

        // Sección 4 y 5: Exploración, Diagnóstico CIE-10 y Plan
        public string? ExploracionFisica { get; set; }
        public string Diagnostico { get; set; } = null!;
        public string? CodigoCIE10 { get; set; }
        public string PlanTerapeutico { get; set; } = null!;
        public string? RecetaMedicamentos { get; set; }
        public string? EstudiosAuxiliares { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}