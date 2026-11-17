namespace Clinica.DTOs
{
    public class NotaMedicaRespuestaDTO
    {
        public int Id { get; set; }

        // Datos relacionales limpios
        public string NombrePaciente { get; set; } = null!;
        public string NombreDoctor { get; set; } = null!;

        // Datos clínicos esenciales
        public string MotivoConsulta { get; set; } = null!;
        public string Diagnostico { get; set; } = null!;
        public string? CodigoCIE10 { get; set; }
        public string PlanTerapeutico { get; set; } = null!;
        public DateTime FechaRegistro { get; set; }
    }
}