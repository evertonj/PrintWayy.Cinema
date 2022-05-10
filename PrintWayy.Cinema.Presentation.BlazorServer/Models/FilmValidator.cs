using FluentValidation;

namespace PrintWayy.Cinema.Presentation.BlazorServer.Models
{
    public class FilmValidator : AbstractValidator<Film>
    {
        [Obsolete]
        public FilmValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(film => film.Title).NotEmpty().WithMessage(Domain.Models.Film.DeveInformarTitulo);
            RuleFor(film => film.Duration).NotEmpty().WithMessage(Domain.Models.Film.DeveInformarDuracao);
            RuleFor(film => film.ImageBase64).NotEmpty().WithMessage(Domain.Models.Film.DeveConterUmaImageNoFormatoStringBase64);
            RuleFor(film => film.Description).NotEmpty().WithMessage(Domain.Models.Film.DeveInformarDescricao);
        }
    }
}
