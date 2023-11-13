using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace NoteKeeper.Infra.Orm.Compartilhado
{
    public class NoteKeeperDbContextFactory : IDesignTimeDbContextFactory<NoteKeeperDbContext>
    {
        public NoteKeeperDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<NoteKeeperDbContext>();

            optionsBuilder.UseSqlServer(@"Data Source=(LOCALDB)\MSSQLLOCALDB;Initial Catalog=NoteKeeper;Integrated Security=True");

            var dbContext = new NoteKeeperDbContext(optionsBuilder.Options);

            return dbContext;
        }
    }
}
