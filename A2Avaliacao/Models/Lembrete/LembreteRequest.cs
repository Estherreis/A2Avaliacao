using System.ComponentModel.DataAnnotations;

namespace A2Avaliacao.Models.Lembrete
{
    public class LembreteRequest
    {
        [Required]
        public DateTime DataHora { get; set; }
        [Required]
        [StringLength(200)]
        public string Mensagem { get; set; }
        public int MembroId { get; set; }
    }
}
