using System.ComponentModel.DataAnnotations;

namespace imaginemos_tecnica.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string DNI { get; set; }
        public object Ventas { get; internal set; }
    }
}
