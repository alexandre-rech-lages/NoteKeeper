using NoteKeeper.Dominio.Compartilhado;
using NoteKeeper.Dominio.ModuloCategoria;

namespace NoteKeeper.Dominio.ModuloNota
{
    public class Nota : Entidade
    {
        public string Titulo { get; set; }

        public string Conteudo { get; set; }

        public TemaEnum Tema { get; set; }

        public bool Arquivada { get; set; }

        public Categoria Categoria { get; set; }

        public Guid CategoriaId { get; set; }
    }
}
