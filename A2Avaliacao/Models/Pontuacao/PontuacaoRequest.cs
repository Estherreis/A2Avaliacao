using System.ComponentModel.DataAnnotations;

namespace A2Avaliacao.Models.Pontuacao
{
    public class PontuacaoRequest
    {
        [Required]
        public int Pontos { get; set; }
        [Required]
        [StringLength(200)]
        public string Descricao { get; set; }
        [Required]
        public int TarefaId { get; set; }
    }
}
