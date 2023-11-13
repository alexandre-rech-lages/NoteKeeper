using NoteKeeper.Dominio.ModuloCategoria;
using NoteKeeper.Dominio.ModuloNota;

namespace NoteKeeper.WebApi.ViewModels
{
    public class ListarNotaViewModel
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Tema { get; set; }
    }

    public class VisualizarNotaViewModel
    {
        public string Titulo { get; set; }

        public string Conteudo { get; set; }

        public string Tema { get; set; }

        public bool Arquivada { get; set; }

        public ListarCategoriaViewModel Categoria { get; set; }

    }

    public class FormsNotaViewModel
    {
        public string Titulo { get; set; }

        public string Conteudo { get; set; }

        public int Tema { get; set; }

        public bool Arquivada { get; set; }

        public Guid CategoriaId { get; set; }
    }

    public class InserirNotaViewModel : FormsNotaViewModel
    {       
    }

    public class EditarNotaViewModel : FormsNotaViewModel
    {      
    }
}
