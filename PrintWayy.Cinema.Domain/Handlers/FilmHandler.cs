using MediatR;
using PrintWayy.Cinema.Domain.Commands.Requests;
using PrintWayy.Cinema.Domain.Commands.Responses;
using PrintWayy.Cinema.Domain.Interfaces;
using PrintWayy.Cinema.Domain.Models;
using PrintWayy.Cinema.Domain.Requests;
using PrintWayy.Cinema.Domain.Responses;
using PrintWayy.Cinema.Domain.Validation.Interfaces;

namespace PrintWayy.Cinema.Domain.Handlers
{
    public class FilmHandler : IRequestHandler<CreateFilmRequest, CreateFilmResponse>,
         IRequestHandler<DeleteFilmRequest, DeleteFilmResponse>,
        IRequestHandler<UpdateFilmRequest, UpdateFilmResponse>
    {
        private readonly IFilmRepository _filmRepository;
        private readonly IValidationCreateFilm _validationCreateFilm;

        public FilmHandler(IFilmRepository filmeRepository, IValidationCreateFilm validationCreateFilm)
        {
            _filmRepository = filmeRepository;
            _validationCreateFilm = validationCreateFilm;
        }

        public Task<CreateFilmResponse> Handle(CreateFilmRequest request, CancellationToken cancellationToken)
        {
            //Filmes não podem ter títulos repetidos.
            var film = _filmRepository.GetFilmByTitle(request.Title);
            if (film != null)
                return Task.FromResult(new CreateFilmResponse
                {
                    Id = film.Id,
                    ImagePath = film.ImagePath,
                    Title = film.Title,
                    Description = film.Description,
                    Duration = film.Duration
                });

            //Cria o filme
            film = new Film(request.ImagePath, request.Title, request.Description, request.Duration);

            //Salva no repositório
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

        public Task<DeleteFilmResponse> Handle(DeleteFilmRequest request, CancellationToken cancellationToken)
        {
            //Um filme só pode ser excluído se não houver sessões vinculadas.

            _filmRepository.Delete(request.Id);

            return Task.FromResult(new DeleteFilmResponse { Success = true });
        }

        public Task<UpdateFilmResponse> Handle(UpdateFilmRequest request, CancellationToken cancellationToken)
        {
            //Filmes não podem ter títulos repetidos.
            var film = _filmRepository.GetFilmByTitle(request.Title);
            if (film != null)
                return Task.FromResult(new UpdateFilmResponse
                {
                    Id = film.Id,
                    ImagePath = film.ImagePath,
                    Title = film.Title,
                    Description = film.Description,
                    Duration = film.Duration
                });

            film = new Film(request.ImagePath, request.Title, request.Description, request.Duration);

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
    }
}
