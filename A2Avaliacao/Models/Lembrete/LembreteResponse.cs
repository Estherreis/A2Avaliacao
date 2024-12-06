using System.Text.Json.Serialization;

namespace A2Avaliacao.Models.Lembrete
{
    public class LembreteResponse
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public string Mensagem { get; set; }
        public string Membro => CarregarMembro();

        [JsonIgnore]
        public string NomeMembro { get; set; }

        [JsonIgnore]
        public string EmailMembro { get; set; }

        private string CarregarMembro()
        {
            return $"{NomeMembro} - {EmailMembro}";
        }
    }
}
