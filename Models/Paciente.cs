using System.ComponentModel.DataAnnotations.Schema;

namespace Clinica.Models
{
    public class Paciente
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("usuarioid")]
        public int? UsuarioId { get; set; } // Lo dejamos nulable por ahora como dijiste

        [Column("nombrecompleto")]
        public string NombreCompleto { get; set; } = string.Empty;

        [Column("telefono")]
        public string Telefono { get; set; } = string.Empty;

        [Column("edad")]
        public int Edad { get; set; }

        [Column("sexo")]
        public string Sexo { get; set; } = string.Empty;

        [Column("alergias")]
        public string Alergias { get; set; } = "Ninguna";

        [Column("fechanacimiento")]
        public DateTime? FechaNacimiento { get; set; }

        [Column("historialmedico")]
        public string? HistorialMedico { get; set; }

        [Column("clinicaid")]
        public int ClinicaId { get; set; } = 1;
    }
}