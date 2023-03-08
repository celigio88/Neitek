using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Metas
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(80)]
        public string Nombre { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public int TotalTareas { get; set; } = 0;
        public double PorcentajeTarea { get; set; } = 0;

        public List<Tareas>? lTareas {get; set; }
    }
}