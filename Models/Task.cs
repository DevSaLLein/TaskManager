using System.ComponentModel.DataAnnotations;
using TaskManager.Enums;

namespace TaskManager.Models
{
    public class TaskItem
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public string Titulo { get; set; }
        
        public string Descricao { get; set; }
        
        public DateTime Data { get; set; }
        
        public StatusEnum Status { get; set; }
    }
}
