namespace GerenciadorFilmes.Models
{
    public class Filme
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public int AnoLancamento { get; set; }
        public int DuracaoMinutos { get; set; }
        public int GeneroId { get; set; }
        public Genero? Genero { get; set; }
    }
}