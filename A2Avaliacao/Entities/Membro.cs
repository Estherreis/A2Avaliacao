using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace A2Avaliacao.Entities
{
    public class Membro
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public ICollection<MembroTarefa>? MembroTarefas { get; set; }
        public ICollection<MembroReuniao>? MembroReunioes { get; set; }
        public ICollection<Pontuacao>? Pontuacoes { get; set; }
        public ICollection<Lembrete>? Lembretes { get; set; }
    }
}
