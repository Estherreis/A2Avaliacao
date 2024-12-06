namespace A2Avaliacao.Models.Pontuacao
{
    public class PontuacaoResponse
    {
        public int Id { get; set; }
        public int Pontos { get; set; }
        public string Descricao { get; set; }
        public object Tarefa { get; set; }
    }
}
