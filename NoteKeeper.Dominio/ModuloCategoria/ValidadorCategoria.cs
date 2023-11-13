using FluentValidation;

namespace NoteKeeper.Dominio.ModuloCategoria
{
    public class ValidadorCategoria : AbstractValidator<Categoria>
    {
        public ValidadorCategoria()
        {
            RuleFor(x => x.Titulo)
                .NotNull().NotEmpty();
        }
    }
}
