namespace A2Avaliacao.Models.Reuniao
{
    public class ReuniaoResponse
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public string Titulo { get; set; }
        public string Notas { get; set; }
        public object Membros { get; set; }
    }
}
