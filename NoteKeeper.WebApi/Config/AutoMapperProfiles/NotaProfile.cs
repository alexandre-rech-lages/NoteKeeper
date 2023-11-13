using AutoMapper;
using NoteKeeper.Dominio.ModuloNota;
using NoteKeeper.WebApi.ViewModels;

namespace NoteKeeper.WebApi.Config.AutoMapperProfiles
{
    public class NotaProfile : Profile
    {
        public NotaProfile()
        {
            CreateMap<Nota, ListarNotaViewModel>();
        }
    }
}
