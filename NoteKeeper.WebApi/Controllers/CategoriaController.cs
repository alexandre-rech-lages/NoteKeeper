using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NoteKeeper.Aplicacao.ModuloCategoria;
using NoteKeeper.Dominio.ModuloCategoria;
using NoteKeeper.WebApi.ViewModels;

namespace NoteKeeper.WebApi.Controllers
{
    [Route("api/categorias")]
    [ApiController]
    public class CategoriaController : ApiControllerBase
    {
        private readonly ServicoCategoria servicoCategoria;
        private readonly IMapper mapeador;

        public CategoriaController(ServicoCategoria servicoCategoria, IMapper mapeador)
        {
            this.servicoCategoria = servicoCategoria;
            this.mapeador = mapeador;
        }

        [HttpGet]

        [ProducesResponseType(typeof(List<ListarCategoriaViewModel>), 200)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SelecionarTodos()
        {
            var categoriasResult = await servicoCategoria.SelecionarTodosAsync();

            var viewModel = mapeador.Map<List<ListarCategoriaViewModel>>(categoriasResult.Value);

            return Ok(viewModel);
        }

        [HttpGet("visualiacao-completa/{id}")]
        [ProducesResponseType(typeof(VisualizarCategoriaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SelecionarPorId(Guid id)
        {
            var categoriaResult = await servicoCategoria.SelecionarPorIdAsync(id);

            if (categoriaResult.IsFailed)
                return NotFound(categoriaResult.Errors);

            var viewModel = mapeador.Map<VisualizarCategoriaViewModel>(categoriaResult.Value);

            return Ok(viewModel);
        }

        [HttpPost]
        [ProducesResponseType(typeof(InserirCategoriaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Inserir(InserirCategoriaViewModel viewModel)
        {
            var categoria = mapeador.Map<Categoria>(viewModel);

            var categoriaResult = await servicoCategoria.InserirAsync(categoria);

            if (categoriaResult.IsFailed)
                return BadRequest(categoriaResult.Errors);

            return Ok(viewModel);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(EditarCategoriaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Editar(Guid id, EditarCategoriaViewModel viewModel)
        {
            var selecacaoCategoriaResult = await servicoCategoria.SelecionarPorIdAsync(id);

            if (selecacaoCategoriaResult.IsFailed)
                return NotFound(selecacaoCategoriaResult.Errors);

            var categoria = mapeador.Map(viewModel, selecacaoCategoriaResult.Value);

            var categoriaResult =  await servicoCategoria.EditarAsync(categoria);

            if (categoriaResult.IsFailed)
                return BadRequest(categoriaResult.Errors);

            return Ok(viewModel);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Excluir(Guid id)
        {            
            var categoriaResult = await servicoCategoria.ExcluirAsync(id);

            if (categoriaResult.IsFailed)
                return NotFound(categoriaResult.Errors);

            return Ok();
        }

    }
}
