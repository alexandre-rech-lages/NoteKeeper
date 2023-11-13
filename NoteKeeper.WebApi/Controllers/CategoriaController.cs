using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NoteKeeper.Aplicacao.ModuloCategoria;
using NoteKeeper.WebApi.ViewModels;

namespace NoteKeeper.WebApi.Controllers
{
    [Route("api/categorias")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ServicoCategoria servicoCategoria;
        private readonly IMapper mapeador;

        public CategoriaController(ServicoCategoria servicoCategoria, IMapper mapeador)
        {
            this.servicoCategoria = servicoCategoria;
            this.mapeador = mapeador;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categorias = await servicoCategoria.SelecionarTodosAsync();

            var viewModel = mapeador.Map<List<ListarCategoriaViewModel>>(categorias.Value);

            return Ok(viewModel);
        }
    }
}
