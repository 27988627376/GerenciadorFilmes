using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorFilmes.ViewModels
{
    public class FilmeFormularioViewModel
    {
        [Display(Name = "Título")]
        [Required(ErrorMessage = "Informe o nome do filme.")]
        [StringLength(100, ErrorMessage = "O título deve ter no máximo 100 caracteres.")]
        public string Titulo { get; set; } = string.Empty;

        [Display(Name = "Duração")]
        [Range(20, 1000, ErrorMessage = "A duração deve estar entre 20 e 1000 minutos.")]
        public int Duracao { get; set; }

        [Display(Name = "Ano")]
        [Range(1888, 2100, ErrorMessage = "Digite o ano de lançamento.")]
        public int AnoLancamento { get; set; }

        [Display(Name = "Gênero")]
        [Required(ErrorMessage = "Informe o genero do filme.")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione um gênero.")]
        public int? GeneroId { get; set; }

        public List<SelectListItem> Genero { get; set; } = [];
    }
}