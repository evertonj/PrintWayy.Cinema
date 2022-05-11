using MediatR;
using PrintWayy.Cinema.Domain.Commands.Requests.Session;
using PrintWayy.Cinema.Domain.Commands.Responses.Session;
using PrintWayy.Cinema.Domain.Interfaces;
using PrintWayy.Cinema.Domain.Models;
using PrintWayy.Cinema.Domain.Validations;

namespace PrintWayy.Cinema.Domain.Handlers
{
    public class SessionHandler : IRequestHandler<CreateSessionRequest, CreateSessionResponse>,
        IRequestHandler<DeleteSessionRequest, DeleteSessionResponse>
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IFilmRepository _filmRepository;
        private readonly ValidationSession _validationSession;

        public SessionHandler(ISessionRepository sessionRepository, IFilmRepository filmRepository)
        {
            _sessionRepository = sessionRepository;
            _filmRepository = filmRepository;
            _validationSession = new ValidationSession(_sessionRepository);
        }

        public Task<CreateSessionResponse> Handle(CreateSessionRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //Convert o StartTime
                TimeSpan startTime;
                try
                {
                    var time = request.StartTime.Split(':');
                    startTime = new TimeSpan(Convert.ToUInt16(time[0]), Convert.ToUInt16(time[1]), Convert.ToUInt16(time[2]));
                }
                catch (Exception)
                {
                    return Task.FromResult(new CreateSessionResponse { ErrorMessage = "Hora de início da sessão com formato incorreto." });
                }

                //Busca o filme
                Film film = _filmRepository.FindById(request.FilmId);
                if (film == null)
                    return Task.FromResult(new CreateSessionResponse { ErrorMessage = "Id do filme informado não corresponde a nenhum filme cadastrado no sistema." });

                //Busca a sala
                var room = Room.GetRooms().Find(r=>r.Name == request.RoomName);
                if (room == null)
                    return Task.FromResult(new CreateSessionResponse { ErrorMessage = "A sala informada não corresponde a nenhuma sala cadastrada no sistema." });

                var session = new Session(request.Date, startTime, request.EntryValue, request.AnimationType, request.AudioType, film, room);

                //a mesma sala não pode passar dois ou mais filmes ao mesmo tempo.
                if (_validationSession.ValidateRoom(session.Room, session.Date, session.StartTime, session.EndTime))
                    return Task.FromResult(new CreateSessionResponse { ErrorMessage = Session.SalaVinculadaSessaoMesmoHorario });

                _sessionRepository.Create(session);

                return Task.FromResult(new CreateSessionResponse
                {
                    Success = true,
                    Id = session.Id,
                    Date = session.Date,
                    StartTime = session.StartTime.ToString(Film.DURATION_PATTERN),
                    AnimationType = session.AnimationType,
                    AudioType = session.AudioType,
                    FilmId = session.Film.Id,
                    RoomName = session.Room.Name,
                    EndTime = session.EndTime,
                    EntryValue = session.EntryValue,
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new CreateSessionResponse { ErrorMessage = ex.Message });
            }
        }


        public Task<DeleteSessionResponse> Handle(DeleteSessionRequest request, CancellationToken cancellationToken)
        {
            var session = _sessionRepository.FindById(request.Id);

            if (session == null)
                return Task.FromResult(new DeleteSessionResponse { ErrorMessage = Session.SessaoNaoEncontadaNaBaseDeDados });

            //Uma sessão só pode ser removida se faltar 10 dias ou mais para que ela ocorra.
            if (_validationSession.ValidateTimeOfRemove(session.Date))
                return Task.FromResult(new DeleteSessionResponse { ErrorMessage = Session.SesssaoNaoPodeSerRemovidaSeFaltar10DiasOuMenos });

            _sessionRepository.Delete(session.Id);

            return Task.FromResult(new DeleteSessionResponse { Success = true });
        }

    }
}
