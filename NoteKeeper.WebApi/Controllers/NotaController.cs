using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoteKeeper.Aplicacao.ModuloCategoria;
using NoteKeeper.Aplicacao.ModuloNota;
using NoteKeeper.Dominio.ModuloCategoria;
using NoteKeeper.Dominio.ModuloNota;
using NoteKeeper.WebApi.ViewModels;

namespace NoteKeeper.WebApi.Controllers
{
    [Route("api/notas")]
    [ApiController]
    public class NotaController : ControllerBase
    {
        private readonly ServicoNota servicoNota;
        private readonly IMapper mapeador;

        public NotaController(ServicoNota servicoNota, IMapper mapeador)
        {
            this.servicoNota = servicoNota;
            this.mapeador = mapeador;
        }

        [HttpGet]
        public async Task<IActionResult> SelecionarTodos()
        {
            var notasResult = await servicoNota.SelecionarTodosAsync();

            var viewModel = mapeador.Map<List<ListarNotaViewModel>>(notasResult.Value);

            return Ok(viewModel);
        }

        [HttpGet("visualiacao-completa/{id}")]
        public async Task<IActionResult> SelecionarPorId(Guid id)
        {
            var notaResult = await servicoNota.SelecionarPorIdAsync(id);

            var viewModel = mapeador.Map<VisualizarNotaViewModel>(notaResult.Value);

            return Ok(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Inserir(InserirNotaViewModel viewModel)
        {
            var nota = mapeador.Map<Nota>(viewModel);

            await servicoNota.InserirAsync(nota);

            return Ok(viewModel);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(Guid id, EditarNotaViewModel viewModel)
        {
            var notaResult = await servicoNota.SelecionarPorIdAsync(id);

            var nota = mapeador.Map(viewModel, notaResult.Value);

            await servicoNota.EditarAsync(nota);

            return Ok(viewModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(Guid id)
        {
            await servicoNota.ExcluirAsync(id);

            return Ok();
        }

    }
}
