using GerenciadorFilmes.Models;

namespace GerenciadorFilmes.ViewModels
{
    public class FilmesIndexViewModel
    {
        public List<Filme> Filmes { get; set; } = [];
        public string? TextoPesquisa { get; set; }
        public int QuantidadeTotal { get; set; }
        public string? OrdenarPor { get; set; }
    }
}