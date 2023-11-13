namespace NoteKeeper.Dominio.Compartilhado
{
    public interface IRepositorioBase<TEntidade> where TEntidade : Entidade
    {
        Task<bool> InserirAsync(TEntidade registro);
        void Editar(TEntidade registro);
        void Excluir(TEntidade registro);
        Task<TEntidade> SelecionarPorIdAsync(Guid id);
        Task<List<TEntidade>> SelecionarTodosAsync();
    }

}
