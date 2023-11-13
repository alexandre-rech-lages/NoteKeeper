using NoteKeeper.Dominio.Compartilhado;
using NoteKeeper.Dominio.ModuloNota;
using NoteKeeper.Infra.Orm.Compartilhado;

namespace NoteKeeper.Infra.Orm.ModuloNota
{
    public class RepositorioNotaOrm : RepositorioBase<Nota>, IRepositorioNota
    {
        public RepositorioNotaOrm(IContextoPersistencia dbContext) : base(dbContext)
        {
        }
    }
}
