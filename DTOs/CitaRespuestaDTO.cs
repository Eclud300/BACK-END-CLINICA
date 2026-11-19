namespace Clinica.DTOs
{
    public class CitaRespuestaDTO
    {
        public int Id { get; set; }
        public string? Folio { get; set; }

        // Datos aplanados del Paciente
        public int PacienteId { get; set; }
        public string NombrePaciente { get; set; } = null!;
        public string CurpPaciente { get; set; } = null!;

        // Datos aplanados del Doctor
        public int DoctorId { get; set; }
        public string NombreDoctor { get; set; } = null!;
        public string RolDoctor { get; set; } = null!;

        // Datos de la Cita
        public DateTime FechaHora { get; set; }
        public string? MotivoConsulta { get; set; }
        public string Estado { get; set; } = null!;
        public string? NotasRecepcion { get; set; }
    }
}