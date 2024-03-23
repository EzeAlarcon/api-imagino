using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace imaginemos_tecnica.Models
{
    public class Venta
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public decimal Total { get; set; }
    }
}
