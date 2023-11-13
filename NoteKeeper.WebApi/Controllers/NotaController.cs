using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NoteKeeper.Aplicacao.ModuloNota;
using NoteKeeper.Dominio.ModuloNota;
using NoteKeeper.WebApi.ViewModels;

namespace NoteKeeper.WebApi.Controllers
{
    [Route("api/notas")]
    [ApiController]
    public class NotaController : ApiControllerBase
    {
        private readonly ServicoNota servicoNota;
        private readonly IMapper mapeador;

        public NotaController(ServicoNota servicoNota, IMapper mapeador)
        {
            this.servicoNota = servicoNota;
            this.mapeador = mapeador;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ListarNotaViewModel>) ,200)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SelecionarTodos()
        {
            var notasResult = await servicoNota.SelecionarTodosAsync();

            var viewModel = mapeador.Map<List<ListarNotaViewModel>>(notasResult.Value);

            return Ok(viewModel);
        }

        [HttpGet("visualiacao-completa/{id}")]
        [ProducesResponseType(typeof(VisualizarNotaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> SelecionarPorId(Guid id)
        {
            var notaResult = await servicoNota.SelecionarPorIdAsync(id);

            if (notaResult.IsFailed)
                return NotFound(notaResult.Errors);

            var viewModel = mapeador.Map<VisualizarNotaViewModel>(notaResult.Value);

            return Ok(viewModel);
        }


        [HttpPost]
        [ProducesResponseType(typeof(InserirNotaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Inserir(InserirNotaViewModel viewModel)
        {
            var nota = mapeador.Map<Nota>(viewModel);

            var notaResult = await servicoNota.InserirAsync(nota);

            if (notaResult.IsFailed)
                return BadRequest(notaResult.Errors);

            return Ok(viewModel);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(typeof(EditarNotaViewModel), 200)]
        [ProducesResponseType(typeof(string[]), 400)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Editar(Guid id, EditarNotaViewModel viewModel)
        {
            var selecaoNotaResult = await servicoNota.SelecionarPorIdAsync(id);

            if (selecaoNotaResult.IsFailed)
                return NotFound(selecaoNotaResult.Errors);

            var nota = mapeador.Map(viewModel, selecaoNotaResult.Value);

            var notaResult = await servicoNota.EditarAsync(nota);

            if (notaResult.IsFailed)
                return BadRequest(notaResult.Errors);

            return Ok(viewModel);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string[]), 404)]
        [ProducesResponseType(typeof(string[]), 500)]
        public async Task<IActionResult> Excluir(Guid id)
        {
            var selecaoNotaResult = await servicoNota.SelecionarPorIdAsync(id);

            if (selecaoNotaResult.IsFailed)
                return NotFound(selecaoNotaResult.Errors);

            await servicoNota.ExcluirAsync(id);

            return Ok();
        }

    }
}
