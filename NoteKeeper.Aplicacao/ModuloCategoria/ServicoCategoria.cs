using FluentResults;
using NoteKeeper.Dominio.Compartilhado;
using NoteKeeper.Dominio.ModuloCategoria;

namespace NoteKeeper.Aplicacao.ModuloCategoria
{
    public class ServicoCategoria
    {
        private readonly IRepositorioCategoria repositorioCategoria;
        private readonly IContextoPersistencia contextoPersistencia;

        public ServicoCategoria(IRepositorioCategoria repositorioCategoria, IContextoPersistencia contextoPersistencia)
        {
            this.repositorioCategoria = repositorioCategoria;
            this.contextoPersistencia = contextoPersistencia;
        }

        public async Task<Result<Categoria>> InserirAsync(Categoria categoria)
        {
            var resultadoValidacao = ValidarCategoria(categoria);

            if (resultadoValidacao.IsFailed)
                return Result.Fail(resultadoValidacao.Errors);

            await repositorioCategoria.InserirAsync(categoria);

            await contextoPersistencia.GravarAsync();

            return Result.Ok(categoria);
        }

        public async Task<Result<Categoria>> EditarAsync(Categoria categoria)
        {
            var resultadoValidacao = ValidarCategoria(categoria);

            if (resultadoValidacao.IsFailed)
                return Result.Fail(resultadoValidacao.Errors);

            repositorioCategoria.Editar(categoria);

            await contextoPersistencia.GravarAsync();

            return Result.Ok(categoria);
        }

        public async Task<Result> ExcluirAsync(Guid id)
        {
            var categoria = await repositorioCategoria.SelecionarPorIdAsync(id);

            repositorioCategoria.Excluir(categoria);

            await contextoPersistencia.GravarAsync();

            return Result.Ok();
        }

        public async Task<Result<List<Categoria>>> SelecionarTodosAsync()
        {
            var categorias = await repositorioCategoria.SelecionarTodosAsync();

            return Result.Ok(categorias);
        }

        public async Task<Result<Categoria>> SelecionarPorIdAsync(Guid id)
        {
            var categoria = await repositorioCategoria.SelecionarPorIdAsync(id);

            return Result.Ok(categoria);
        }

        private Result ValidarCategoria(Categoria categoria)
        {
            ValidadorCategoria validador = new ValidadorCategoria();

            var resultadoValidacao = validador.Validate(categoria);

            List<Error> erros = new List<Error>();

            foreach (var erro in resultadoValidacao.Errors)            
                erros.Add(new Error(erro.ErrorMessage));            

            if (erros.Any())
                return Result.Fail(erros.ToArray());

            return Result.Ok();
        }
    }
}
