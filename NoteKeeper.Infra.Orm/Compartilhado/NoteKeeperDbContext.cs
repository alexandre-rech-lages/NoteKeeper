using Microsoft.EntityFrameworkCore;
using NoteKeeper.Dominio.ModuloCategoria;
using NoteKeeper.Dominio.ModuloNota;
using NoteKeeper.Infra.Orm.ModuloCategoria;
using NoteKeeper.Infra.Orm.ModuloNota;

namespace NoteKeeper.Infra.Orm.Compartilhado
{
    public class NoteKeeperDbContext : DbContext
    {
        public NoteKeeperDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Nota> Notas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MapeadorCategoriaOrm());

            modelBuilder.ApplyConfiguration(new MapeadorNotaOrm());

            base.OnModelCreating(modelBuilder);
        }

    }
}
