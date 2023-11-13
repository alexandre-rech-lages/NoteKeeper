using NoteKeeper.Dominio.ModuloNota;
using NoteKeeper.Infra.Orm.Compartilhado;

namespace NoteKeeper.Infra.Orm.ModuloNota
{
    public class RepositorioNotaOrm : RepositorioBase<Nota>, IRepositorioNota
    {
        public RepositorioNotaOrm(NoteKeeperDbContext dbContext) : base(dbContext)
        {
        }
    }
}
