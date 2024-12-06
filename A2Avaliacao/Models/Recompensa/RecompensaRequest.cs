using System.ComponentModel.DataAnnotations;

namespace A2Avaliacao.Models.Recompensa
{
    public class RecompensaRequest
    {
        [Required]
        [StringLength(100)]
        public string Nome { get; set; }
        [StringLength(500)]
        public string Descricao { get; set; }
        [Required]
        public int CustoEmPontos { get; set; }
    }
}
