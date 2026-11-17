using System.ComponentModel.DataAnnotations.Schema;

namespace Clinica.Models
{
    public class Cita
    {
        [Column("id")]
        public int Id { get; set; }
        
        [Column("clinicaid")]
        public int ClinicaId { get; set; }
        
        [Column("pacienteid")]
        public int PacienteId { get; set; }
        
        [Column("folio")]
        public string Folio { get; set; } = string.Empty;
        
        [Column("fechahora")]
        public DateTime FechaHora { get; set; }
        
        [Column("estado")]
        public string Estado { get; set; } = string.Empty;
    }
}