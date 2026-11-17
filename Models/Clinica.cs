using System.ComponentModel.DataAnnotations.Schema;

namespace Clinica.Models
{
    public class Clinica
    {
        [Column("id")]
        public int Id { get; set; }
        
        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;
        
        [Column("logo")]
        public string Logo { get; set; } = string.Empty;
        
        [Column("direccion")]
        public string Direccion { get; set; } = string.Empty;
        
        [Column("telefono")]
        public string Telefono { get; set; } = string.Empty;
    }
}