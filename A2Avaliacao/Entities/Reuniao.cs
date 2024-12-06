using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace A2Avaliacao.Entities
{
    public class Reuniao
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public string Titulo { get; set; }
        public string Notas { get; set; }

        [JsonIgnore]
        public ICollection<MembroReuniao>? MembroReunioes { get; set; }
    }
}
