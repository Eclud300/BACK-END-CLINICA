using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinica.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Correo { get; set; } = null!;

        [Required]
        public string PasswordHash { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string Rol { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string Estado { get; set; } = "Activo"; // Activo, Suspendido

        // Datos personales y de contacto (Opcionales)
        [MaxLength(20)]
        public string? Telefono { get; set; }
        public DateTime? FechaNacimiento { get; set; }

        // Datos Profesionales
        public string? Estudios { get; set; }
        [MaxLength(150)]
        public string? UniversidadEgreso { get; set; }
        [MaxLength(50)]
        public string? CedulaProfesional { get; set; }
        [MaxLength(50)]
        public string? CedulaEspecialidad { get; set; }
        public string? FirmaDigital { get; set; }

        // Finanzas
        public decimal? PorcentajeComision { get; set; }
        public decimal? TarifaConsulta { get; set; }

        // Seguridad
        public DateTime? FechaFirmaPrivacidad { get; set; }
        public DateTime? UltimoLogin { get; set; }
        public bool Requiere2FA { get; set; } = false;
        [MaxLength(255)]
        public string? Secreto2FA { get; set; }
        public int IntentosFallidos { get; set; } = 0;
        public DateTime? BloqueadoHasta { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    }
}