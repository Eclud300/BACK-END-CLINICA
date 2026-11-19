namespace Clinica.DTOs
{
    public class UsuarioActualizarDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Rol { get; set; } = null!;
        public string? Estado { get; set; }
        public string? Password { get; set; } // Opcional: solo si se desea cambiar la contraseña
        public string? Telefono { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Estudios { get; set; }
        public string? UniversidadEgreso { get; set; }
        public string? CedulaProfesional { get; set; }
        public string? CedulaEspecialidad { get; set; }
        public string? FirmaDigital { get; set; }
        public decimal? PorcentajeComision { get; set; }
        public decimal? TarifaConsulta { get; set; }
    }
}