using A2Avaliacao.Entities;
using System.ComponentModel.DataAnnotations;

namespace A2Avaliacao.Models.Reuniao
{
    public class ReuniaoRequest
    {
        public DateTime DataHora { get; set; }
        [StringLength(200)]
        public string Titulo { get; set; }
        [StringLength(1000)]
        public string Notas { get; set; }
        public ICollection<int>? MembrosId { get; set; }
    }
}
