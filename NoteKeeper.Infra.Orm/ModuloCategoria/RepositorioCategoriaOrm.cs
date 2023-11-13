using Microsoft.EntityFrameworkCore;
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


        public override async Task<Categoria> SelecionarPorIdAsync(Guid id)
        {
            return await registros.Include(x => x.Notas).SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
