using Microsoft.EntityFrameworkCore;
using NoteKeeper.Dominio.ModuloCategoria;
using NoteKeeper.Dominio.ModuloNota;

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
            modelBuilder.Entity<Categoria>(model =>
            {
                model.ToTable("TBCategoria");

                model.Property(x => x.Id)
                    .ValueGeneratedNever();

                model.Property(x => x.Titulo)
                    .IsRequired();
            });            

            modelBuilder.Entity<Nota>(model =>
            {
                model.ToTable("TBNota");

                model.Property(x => x.Id)
                    .ValueGeneratedNever();

                model.Property(x => x.Titulo)
                    .IsRequired();

                model.Property(x => x.Conteudo)
                  .IsRequired();

                model.Property(x => x.Tema)
                    .HasConversion<int>()
                  .IsRequired();

                model.Property(x => x.Arquivada)                    
                  .IsRequired();

                model.HasOne(x => x.Categoria)
                    .WithMany()
                    .HasForeignKey(x => x.CategoriaId)
                    .HasConstraintName("FK_TBCategoria_TBNota")
                    .OnDelete(DeleteBehavior.NoAction);
            });

            base.OnModelCreating(modelBuilder);
        }

    }
}
