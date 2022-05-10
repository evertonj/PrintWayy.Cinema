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
                var time = request.Duration.Split(':');
                var duration = new TimeSpan(Convert.ToUInt16(time[0]), Convert.ToUInt16(time[1]), Convert.ToUInt16(time[2]));

                var film = new Film(request.ImageBase64, request.Title, request.Description, duration);

                _filmRepository.Create(film);

                return Task.FromResult(new CreateFilmResponse
                {
                    Success = true,
                    Id = film.Id,
                    ImageBase64 = film.ImageBase64,
                    Title = film.Title,
                    Description = film.Description,
                    Duration = film.Duration.ToString(Film.DURATION_PATTERN),
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
                TimeSpan duration = TimeSpan.Zero;
                try
                {
                    var time = request.Duration.Split(':');
                    duration = new TimeSpan(Convert.ToUInt16(time[0]), Convert.ToUInt16(time[1]), Convert.ToUInt16(time[2]));
                }
                catch (Exception)
                {
                    Task.FromResult(new UpdateFilmResponse { ErrorMessage = Film.DeveInformarDuracao });
                }
               
                var film = new Film(request.Id, request.ImageBase64, request.Title, request.Description, duration);
                
                _filmRepository.Update(film);

                return Task.FromResult(new UpdateFilmResponse
                {
                    Success = true,
                    Id = film.Id,
                    ImageBase64 = film.ImageBase64,
                    Title = film.Title,
                    Description = film.Description,
                    Duration = film.Duration.ToString(Film.DURATION_PATTERN),
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new UpdateFilmResponse { ErrorMessage = ex.Message });
            }
        }
    }
}
