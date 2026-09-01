using GerenciadorFilmes.Models;
using GerenciadorFilmes.Services;
using GerenciadorFilmes.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GerenciadorFilmes.Controllers;

public class FilmesController : Controller
{
    private readonly IFilmeService _filmeService;

    public FilmesController(IFilmeService filmeService)
    {
        _filmeService = filmeService;
    }

    public IActionResult Index(string? pesquisa, string? ordenarPor)
    {
        var projetos = _filmeService.PesquisarPorTitulo(pesquisa);
        projetos = _filmeService.Ordenar(projetos, ordenarPor);

        var model = new FilmesIndexViewModel
        {
            Filmes = projetos,
            TextoPesquisa = pesquisa,
            QuantidadeTotal = projetos.Count,
            OrdenarPor = ordenarPor
        };

        return View(model);
    }

    public IActionResult Detalhes(int id)
    {
        var projeto = _filmeService.ObterPorId(id);

        if (projeto is null)
            return NotFound();

        return View(projeto);
    }

    // =========================
    // CADASTRAR
    // =========================

    [HttpGet]
    public IActionResult Cadastrar()
    {
        var model = new NovoFilmeViewModel
        {
            Genero = ObterGenerosSelectList()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Cadastrar(NovoFilmeViewModel model)
    {
        // Recarrega os generos caso o formulário tenha erro
        model.Genero = ObterGenerosSelectList();

        if (!ModelState.IsValid)
            return View(model);

        _filmeService.Adicionar(model);

        TempData["Mensagem"] = "Filme cadastrado com sucesso!";

        return RedirectToAction(nameof(Index));
    }

    // =========================
    // EDITAR
    // =========================

    [HttpGet]
    public IActionResult Editar(int id)
    {
        var filme = _filmeService.ObterPorId(id);

        if (filme is null)
            return NotFound();

        var model = new EditarFilmeViewModel
        {
            Id = filme.Id,
            Titulo = filme.Titulo,
            AnoLancamento = filme.AnoLancamento,
            Duracao = filme.DuracaoMinutos,
            GeneroId = filme.GeneroId,

            // Carrega os generos para o <select>
            Genero = ObterGenerosSelectList()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Editar(EditarFilmeViewModel model)
    {
        // Recarrega os generos caso o formulário tenha erro
        model.Genero = ObterGenerosSelectList();

        if (!ModelState.IsValid)
            return View(model);

        var atualizado = _filmeService.Atualizar(model);

        if (!atualizado)
            return NotFound();

        TempData["Mensagem"] = "Projeto atualizado com sucesso!";

        return RedirectToAction(nameof(Index));
    }

    // =========================
    // EXCLUIR
    // =========================

    [HttpGet]
    public IActionResult Excluir(int id)
    {
        var filme = _filmeService.ObterPorId(id);

        if (filme is null)
            return NotFound();

        return View(filme);
    }

    [HttpPost, ActionName("Excluir")]
    [ValidateAntiForgeryToken]
    public IActionResult ConfirmarExclusao(int id)
    {
        var removido = _filmeService.Remover(id);

        if (!removido)
            return NotFound();

        TempData["Mensagem"] = "Filme excluído com sucesso!";

        return RedirectToAction(nameof(Index));
    }

    // =========================
    // PROFESSORES
    // =========================

    private List<SelectListItem> ObterGenerosSelectList()
    {
        return _filmeService.ListarGenero()
            .Select(genero => new SelectListItem
            {
                Value = genero.Id.ToString(),
                Text = genero.Nome
            })
            .ToList();
    }
}