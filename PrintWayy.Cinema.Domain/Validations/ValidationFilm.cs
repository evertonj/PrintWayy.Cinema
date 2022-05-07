using PrintWayy.Cinema.Domain.Interfaces;

namespace PrintWayy.Cinema.Domain.Validations
{
    public class ValidationFilm
    {
        private readonly IFilmRepository _filmRepository;

        public ValidationFilm(IFilmRepository filmRepository)
        {
            _filmRepository = filmRepository;
        }

        public bool ValidateCreateFilmResponse(string title)
        {
            var film = _filmRepository.GetFilmByTitle(title);
            return film != null;
        }
    }
}
