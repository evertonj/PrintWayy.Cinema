using PrintWayy.Cinema.Domain.Interfaces;
using PrintWayy.Cinema.Domain.Models;
using PrintWayy.Cinema.Domain.Requests;
using PrintWayy.Cinema.Domain.Validation.Interfaces;

namespace PrintWayy.Cinema.Domain.Validation
{
    public class ValidationCreateFilm : IValidationCreateFilm
    {
        private readonly IFilmRepository _filmRepository;

        public ValidationCreateFilm(IFilmRepository filmRepository)
        {
            _filmRepository = filmRepository;
        }

        public Film NotRepeatTitle(CreateFilmRequest createFilmRequest)
        {
            throw new NotImplementedException();
        }
    }
}
