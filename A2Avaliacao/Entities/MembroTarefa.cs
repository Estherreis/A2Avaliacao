namespace A2Avaliacao.Entities
{
    public class MembroTarefa
    {
        public int Id { get; set; }
        public int MembroId { get; set; }
        public Membro Membro { get; set; }
        public int TarefaId { get; set; }
        public Tarefa Tarefa { get; set; }
    }
}
