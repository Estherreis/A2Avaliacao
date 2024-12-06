using System.ComponentModel.DataAnnotations;

namespace A2Avaliacao.Entities
{
    public class Recompensa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int CustoEmPontos { get; set; }
    }
}
