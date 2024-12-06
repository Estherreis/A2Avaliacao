using System.ComponentModel.DataAnnotations;

namespace A2Avaliacao.Entities
{
    public class Pontuacao
    {
        public int Id { get; set; }
        public int Pontos { get; set; }
        public string Descricao { get; set; }
        public int TarefaId { get; set; }
        public Tarefa? Tarefa { get; set; }
    }
}
