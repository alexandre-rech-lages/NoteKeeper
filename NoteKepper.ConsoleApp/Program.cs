using Microsoft.EntityFrameworkCore;
using NoteKeeper.Dominio.ModuloCategoria;
using NoteKeeper.Dominio.ModuloNota;
using NoteKeeper.Infra.Orm.Compartilhado;

namespace NoteKepper.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var novaCategoria = new Categoria();
            novaCategoria.Titulo = "Mercado";

            var optionsBuilder = new DbContextOptionsBuilder<NoteKeeperDbContext>();

            optionsBuilder.UseSqlServer(@"Data Source=(LOCALDB)\MSSQLLOCALDB;Initial Catalog=NoteKeeper;Integrated Security=True");

            var dbContext = new NoteKeeperDbContext(optionsBuilder.Options);

            dbContext.Categorias.Add(novaCategoria);

            var novaNota = new Nota();
            novaNota.Titulo = "Comprar Cerveja";
            novaNota.Conteudo = "Comprar Cerveja para o churrasco de aniversário da minha filhota";
            novaNota.Arquivada = false;
            novaNota.Tema = TemaEnum.Realcada;
            novaNota.Categoria = novaCategoria;

            dbContext.Notas.Add(novaNota);

            dbContext.SaveChanges();
        }
    }
}