using System.ComponentModel.DataAnnotations;

namespace A2Avaliacao.Models.Membro
{
    public class MembroRequest
    {
        [Required]
        [StringLength(100)]
        public string Nome { get; set; }
        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }
    }
}
