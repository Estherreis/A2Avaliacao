using System.ComponentModel.DataAnnotations;

namespace A2Avaliacao.Models.Tarefa
{
    public class TarefaRequest
    {
        [Required]
        [StringLength(100)]
        public string Nome { get; set; }
        [StringLength(500)]
        public string Descricao { get; set; }
        [Required]
        public DateTime DataCriacao { get; set; }
        public DateTime? DataConclusao { get; set; }
        [Required]
        public bool Concluida { get; set; }
    }
}
