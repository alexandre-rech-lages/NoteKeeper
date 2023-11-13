using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NoteKeeper.Aplicacao.ModuloCategoria;
using NoteKeeper.Dominio.ModuloCategoria;
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
        public async Task<IActionResult> SelecionarTodos()
        {
            var categoriasResult = await servicoCategoria.SelecionarTodosAsync();

            var viewModel = mapeador.Map<List<ListarCategoriaViewModel>>(categoriasResult.Value);

            return Ok(viewModel);
        }

        [HttpGet("visualiacao-completa/{id}")]
        public async Task<IActionResult> SelecionarPorId(Guid id)
        {
            var categoriaResult = await servicoCategoria.SelecionarPorIdAsync(id);

            var viewModel = mapeador.Map<VisualizarCategoriaViewModel>(categoriaResult.Value);

            return Ok(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Inserir(InserirCategoriaViewModel viewModel)
        {
            var categoria = mapeador.Map<Categoria>(viewModel);

            await servicoCategoria.InserirAsync(categoria);

            return Ok(viewModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(Guid id, EditarCategoriaViewModel viewModel)
        {
            var categoriaResult = await servicoCategoria.SelecionarPorIdAsync(id);

            var categoria = mapeador.Map(viewModel, categoriaResult.Value);

            await servicoCategoria.EditarAsync(categoria);

            return Ok(viewModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(Guid id)
        {            
            await servicoCategoria.ExcluirAsync(id);

            return Ok();
        }

    }
}
