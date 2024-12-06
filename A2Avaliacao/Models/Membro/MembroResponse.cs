using System.Text.Json.Serialization;

namespace A2Avaliacao.Models.Membro
{
    public class MembroResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public object Lembrete { get; set; }
    }
}
