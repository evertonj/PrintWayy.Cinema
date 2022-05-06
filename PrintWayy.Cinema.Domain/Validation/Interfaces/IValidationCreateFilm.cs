using PrintWayy.Cinema.Domain.Models;
using PrintWayy.Cinema.Domain.Requests;

namespace PrintWayy.Cinema.Domain.Validation.Interfaces
{
    public interface IValidationCreateFilm
    {
        Film NotRepeatTitle(CreateFilmRequest createFilmRequest);
    }
}
