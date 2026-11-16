using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinica.Models
{
    public class Cita
    {
        [Key]
        public int Id { get; set; }
        public string Folio { get; set; }
        public int PacienteId { get; set; }
        public int ClinicaId { get; set; }
        public DateTime FechaHora { get; set; }
        public string Estado { get; set; } // Pendiente, Confirmada, Cancelada
    }
}