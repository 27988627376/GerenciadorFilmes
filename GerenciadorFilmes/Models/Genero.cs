namespace GerenciadorFilmes.Models
{
    public class Genero
    {
        public int? Id { get; set; }
        public string? Nome { get; set; } = string.Empty;
        public List<Filme> Filme { get; set; } = new();
    }
}