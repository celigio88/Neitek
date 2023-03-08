using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Tareas
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(80)]
        public string Nombre { get; set; } = string.Empty;
        public DateTime FechaCreada { get; set; }
        public bool Abierta { get; set; }

        public int MetaId { get; set; }
        public Metas? Meta { get; set; }
    }
}