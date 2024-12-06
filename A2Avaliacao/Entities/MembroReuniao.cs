using System.Text.Json.Serialization;

namespace A2Avaliacao.Entities
{
    public class MembroReuniao
    {
        public int Id { get; set; }
        public int MembroId { get; set; }
        public Membro Membro { get; set; }
        public int ReuniaoId { get; set; }
        public Reuniao Reuniao { get; set; }
    }
}
