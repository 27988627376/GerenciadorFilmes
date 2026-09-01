using GerenciadorFilmes.Models;
using GerenciadorFilmes.ViewModels;


namespace GerenciadorFilmes.Services
{
    public interface IFilmeService
    {
        List<Filme> PesquisarPorTitulo(string? titulo);
        List<Filme> Ordenar(IEnumerable<Filme> filmes, string? ordenarPor);
        List<Filme> Listar();
        Filme? ObterPorId(int id);
        void Adicionar(NovoFilmeViewModel model);
        bool Atualizar(EditarFilmeViewModel model);
        bool Remover(int id);
        List<Genero> ListarGenero();
    }
}