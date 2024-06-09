using System.ComponentModel.DataAnnotations;

namespace api.DTO.Tarefa.Request
{
    public class TarefaRequest
    {
        [Required]
        [MinLength(8, ErrorMessage = "Deve possuir ao menos 8 caracteres")]
        public string Nome { get; set; }   
    }
}