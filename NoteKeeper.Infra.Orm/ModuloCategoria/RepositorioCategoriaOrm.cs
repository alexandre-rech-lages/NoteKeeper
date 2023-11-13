using NoteKeeper.Dominio.Compartilhado;
using NoteKeeper.Dominio.ModuloCategoria;
using NoteKeeper.Infra.Orm.Compartilhado;

namespace NoteKeeper.Infra.Orm.ModuloCategoria
{
    public class RepositorioCategoriaOrm : RepositorioBase<Categoria>, IRepositorioCategoria
    {
        public RepositorioCategoriaOrm(IContextoPersistencia dbContext) : base(dbContext)
        {

        }

    }
}
