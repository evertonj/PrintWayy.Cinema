using FluentValidation;

namespace PrintWayy.Cinema.Presentation.BlazorServer.Models
{
    public class SessionValidator : AbstractValidator<Session>
    {
        [Obsolete]
        public SessionValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(film => film.StartTime).NotEmpty().WithMessage("Informe uma hora de início.");
            RuleFor(film => film.FilmId).NotEmpty().WithMessage("Selecione um filme.");
            RuleFor(film => film.EntryValue).NotEmpty().WithMessage("Informe um valor.");
            RuleFor(film => film.RoomName).NotEmpty().WithMessage("Selecione uma sala");
            RuleFor(film => film.Date).NotEmpty().WithMessage("Informe uma data.");
        }
    }
}
