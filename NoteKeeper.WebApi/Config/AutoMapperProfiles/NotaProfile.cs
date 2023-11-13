using AutoMapper;
using NoteKeeper.Dominio.ModuloCategoria;
using NoteKeeper.Dominio.ModuloNota;
using NoteKeeper.WebApi.ViewModels;

namespace NoteKeeper.WebApi.Config.AutoMapperProfiles
{
    public class NotaProfile : Profile
    {
        public NotaProfile()
        {
            CreateMap<Nota, ListarNotaViewModel>();

            CreateMap<Nota, VisualizarNotaViewModel>();


            CreateMap<FormsNotaViewModel, Nota>()
                .AfterMap<ConfigurarCategoriaMappingAction>();            
        }
    }

    public class ConfigurarCategoriaMappingAction : IMappingAction<FormsNotaViewModel, Nota>
    {
        private readonly IRepositorioCategoria repositorioCategoria;

        public ConfigurarCategoriaMappingAction(IRepositorioCategoria repositorioCategoria)
        {
            this.repositorioCategoria = repositorioCategoria;
        }

        public void Process(FormsNotaViewModel viewModel, Nota nota, ResolutionContext context)
        {
            nota.Categoria = repositorioCategoria.SelecionarPorId(viewModel.CategoriaId);
        }
    }
}
