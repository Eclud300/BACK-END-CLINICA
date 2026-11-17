namespace Clinica.Models
{
    public class Consulta
    {
        public int Id { get; set; }
        public int ClinicaId { get; set; }
        public int PacienteId { get; set; }
        public DateTime FechaConsulta { get; set; } = DateTime.UtcNow;
        public string Diagnostico { get; set; } = string.Empty;
        public string RecetaMedica { get; set; } = string.Empty;
        public int DoctorId { get; set; } 
    }
}