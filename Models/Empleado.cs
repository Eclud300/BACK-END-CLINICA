namespace Clinica.Models
{
    public class Empleado
    {
        public int Id { get; set; }
        public int ClinicaId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public string? Cedula { get; set; }
        public string Estado { get; set; } = "Activo";
    }
}