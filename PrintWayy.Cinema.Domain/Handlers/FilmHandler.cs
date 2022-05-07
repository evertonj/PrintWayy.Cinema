using MediatR;
using PrintWayy.Cinema.Domain.Commands.Requests.Film;
using PrintWayy.Cinema.Domain.Commands.Responses.Film;
using PrintWayy.Cinema.Domain.Interfaces;
using PrintWayy.Cinema.Domain.Models;
using PrintWayy.Cinema.Domain.Validations;

namespace PrintWayy.Cinema.Domain.Handlers
{
    public class FilmHandler : IRequestHandler<CreateFilmRequest, CreateFilmResponse>,
         IRequestHandler<DeleteFilmRequest, DeleteFilmResponse>,
        IRequestHandler<UpdateFilmRequest, UpdateFilmResponse>
    {
        private readonly IFilmRepository _filmRepository;
        private readonly ISessionRepository _sessionRepository;
        private readonly ValidationFilm _validationFilm;

        public FilmHandler(IFilmRepository filmeRepository, ISessionRepository sessionRepository)
        {
            _filmRepository = filmeRepository;
            _sessionRepository = sessionRepository;
            _validationFilm = new ValidationFilm(_filmRepository);
        }

        public Task<CreateFilmResponse> Handle(CreateFilmRequest request, CancellationToken cancellationToken)
        {
            //Filmes não podem ter títulos repetidos.
            if (_validationFilm.ValidateCreateFilmResponse(request.Title))
                return Task.FromResult(new CreateFilmResponse { ErrorMessage = Film.NaoDeveTerTituloRepetido });

            try
            {
                var film = new Film(request.ImagePath, request.Title, request.Description, request.Duration);

                _filmRepository.Create(film);

                return Task.FromResult(new CreateFilmResponse
                {
                    Id = film.Id,
                    ImagePath = film.ImagePath,
                    Title = film.Title,
                    Description = film.Description,
                    Duration = film.Duration
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new CreateFilmResponse { ErrorMessage = ex.Message });
            }
        }

        public Task<DeleteFilmResponse> Handle(DeleteFilmRequest request, CancellationToken cancellationToken)
        {
            var film = _filmRepository.FindById(request.Id);

            if (film == null)
                return Task.FromResult(new DeleteFilmResponse { Success = false, ErrorMessage = Film.FilmeNaoEncontrado });

            //Um filme só pode ser excluído se não houver sessões vinculadas.
            var session = _sessionRepository.GetSessionByFilm(film);
            if (session != null)
                return Task.FromResult(new DeleteFilmResponse { Success = false, ErrorMessage = Film.NaoRemoveFilmeVinculadoSessao });

            _filmRepository.Delete(request.Id);

            return Task.FromResult(new DeleteFilmResponse { Success = true });
        }

        public Task<UpdateFilmResponse> Handle(UpdateFilmRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var film = new Film(request.ImagePath, request.Title, request.Description, request.Duration);

                _filmRepository.Update(film);

                return Task.FromResult(new UpdateFilmResponse
                {
                    Id = film.Id,
                    ImagePath = film.ImagePath,
                    Title = film.Title,
                    Description = film.Description,
                    Duration = film.Duration
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new UpdateFilmResponse { ErrorMessage = ex.Message });
            }
        }
    }
}
