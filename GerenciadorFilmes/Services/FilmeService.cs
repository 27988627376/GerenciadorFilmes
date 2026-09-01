using GerenciadorFilmes.Models;
using GerenciadorFilmes.Services;
using GerenciadorFilmes.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace GerenciadorFilmes.Services
{
    public class FilmeService : IFilmeService
    {
        private int _proximoId = 1;

        private readonly List<Genero> _generos =
        [
            new Genero { Id = 1, Nome = "Ação" },
            new Genero { Id = 2, Nome = "Comédia" },
            new Genero { Id = 3, Nome = "Drama" },
            new Genero { Id = 4, Nome = "Ficção Científica" },
            new Genero { Id = 5, Nome = "Terror" },
            new Genero { Id = 6, Nome = "Animação" }
        ];

        private readonly List<Filme> _filmes =
        [
            new Filme
{
                Id = 1,
                Titulo = "Homem-Aranha",
                AnoLancamento = 2024,
                DuracaoMinutos = 120,
                GeneroId = 1
            },
            new Filme
            {
                Id = 2,
                Titulo = "Batman",
                AnoLancamento = 2024,
                 DuracaoMinutos = 120,
                GeneroId = 2
            }

        ];

        public List<Genero> ListarGenero()
        {
            return _generos;
        }
        public List<Filme> Ordenar(IEnumerable<Filme> filmes, string? ordenarPor)
        {
            return ordenarPor?.ToLowerInvariant() switch
            {
                "titulo" => filmes.OrderBy(filme => filme.Titulo).ToList(),
                "duracao" => filmes.OrderBy(filme => filme.DuracaoMinutos).ToList(),
                _ => filmes.ToList()
            };
        }
        public List<Filme> Listar()
        {
            return _filmes.Select(VincularGenero).ToList();
        }

        private Genero? ObterGeneroPorId(int generoId)
        {
            return _generos.FirstOrDefault(genero => genero.Id == generoId);
        }

        private Filme VincularGenero(Filme filme)
        {
            filme.Genero = ObterGeneroPorId(filme.GeneroId);
            return filme;
        }

        public Filme? ObterPorId(int id)
        {
            var filme = _filmes.Select(VincularGenero).FirstOrDefault(filme => filme.Id == id);

            if (filme is null)
                return null;

            return VincularGenero(filme);
        }

        public void Adicionar(NovoFilmeViewModel model)
        {
            var novoFilme = new Filme
            {
                Id = GerarNovoId(),
                Titulo = model.Titulo,
                DuracaoMinutos = model.Duracao,
                AnoLancamento = model.AnoLancamento,
                GeneroId = model.GeneroId.Value,
                Genero = ObterGeneroPorId(model.GeneroId.Value)
            };

            _filmes.Add(novoFilme);
        }

        public bool Atualizar(EditarFilmeViewModel model)
        {
            var filme = ObterPorId(model.Id);

            if (filme is null)
                return false;

            filme.Titulo = model.Titulo;
            filme.DuracaoMinutos = model.Duracao;
            filme.AnoLancamento = model.AnoLancamento;
            filme.GeneroId = model.GeneroId.Value;
            filme.Genero = ObterGeneroPorId(model.GeneroId.Value);
            return true;
        }


        public bool Remover(int id)
        {
            var filme = ObterPorId(id);

            if (filme is null)
                return false;

            _filmes.Remove(filme);
            return true;
        }

        private int GerarNovoId()
        {
            return _filmes.Count == 0 ? 1 : _filmes.Max(filme => filme.Id) + 1;
        }

        public List<Filme> PesquisarPorTitulo(string? titulo)
        {
            if (string.IsNullOrWhiteSpace(titulo))
                return Listar();

            return _filmes
                .Where(filme => filme.Titulo.Contains(titulo, StringComparison.CurrentCultureIgnoreCase))
                .Select(VincularGenero)
                .ToList();
        }
    }
}