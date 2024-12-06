using System.ComponentModel.DataAnnotations;

namespace A2Avaliacao.Entities
{
    public class Lembrete
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public string Mensagem { get; set; }
        public int MembroId { get; set; }
        public Membro? Membro { get; set; }
    }
}
