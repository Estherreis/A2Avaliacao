using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace A2Avaliacao.Entities
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataConclusao { get; set; }
        public bool Concluida { get; set; }

        [JsonIgnore]
        public ICollection<MembroTarefa>? MembroTarefas { get; set; }

        [JsonIgnore]
        public ICollection<Pontuacao>? Pontuacoes { get; set; }
    }
}
