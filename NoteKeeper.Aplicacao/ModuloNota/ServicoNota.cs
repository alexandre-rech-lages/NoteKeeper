using FluentResults;
using NoteKeeper.Dominio.Compartilhado;
using NoteKeeper.Dominio.ModuloCategoria;
using NoteKeeper.Dominio.ModuloNota;

namespace NoteKeeper.Aplicacao.ModuloNota
{
    public class ServicoNota
    {
        private readonly IRepositorioNota repositorioNota;
        private readonly IContextoPersistencia contextoPersistencia;

        public ServicoNota(IRepositorioNota repositorioNota, IContextoPersistencia contextoPersistencia)
        {
            this.repositorioNota = repositorioNota;
            this.contextoPersistencia = contextoPersistencia;
        }

        public async Task<Result<Nota>> InserirAsync(Nota nota)
        {
            var resultadoValidacao = ValidarNota(nota);

            if (resultadoValidacao.IsFailed)
                return Result.Fail(resultadoValidacao.Errors);

            await repositorioNota.InserirAsync(nota);

            await contextoPersistencia.GravarAsync();

            return Result.Ok(nota);
        }

        public async Task<Result<Nota>> EditarAsync(Nota nota)
        {
            var resultadoValidacao = ValidarNota(nota);

            if (resultadoValidacao.IsFailed)
                return Result.Fail(resultadoValidacao.Errors);

            repositorioNota.Editar(nota);

            await contextoPersistencia.GravarAsync();

            return Result.Ok(nota);
        }

        public async Task<Result<Nota>> ExcluirAsync(Guid id)
        {
            var nota = await repositorioNota.SelecionarPorIdAsync(id);
            
            repositorioNota.Excluir(nota);

            await contextoPersistencia.GravarAsync();

            return Result.Ok();
        }

        public async Task<Result<List<Nota>>> SelecionarTodosAsync()
        {
            var notas = await repositorioNota.SelecionarTodosAsync();

            return Result.Ok(notas);
        }

        public async Task<Result<Nota>> SelecionarPorIdAsync(Guid id)
        {
            var nota = await repositorioNota.SelecionarPorIdAsync(id);

            return Result.Ok(nota);
        }


        private Result ValidarNota(Nota nota)
        {
            ValidadorNota validador = new ValidadorNota();

            var resultadoValidacao = validador.Validate(nota);

            List<Error> erros = new List<Error>();

            foreach (var erro in resultadoValidacao.Errors)
                erros.Add(new Error(erro.ErrorMessage));

            if (erros.Any())
                return Result.Fail(erros.ToArray());

            return Result.Ok();
        }

    }
}
