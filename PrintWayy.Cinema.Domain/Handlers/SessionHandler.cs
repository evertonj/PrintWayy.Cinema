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
                var session = new Session(request.Date, request.StartTime, request.EntryValue, request.AnimationType, request.AudioType, request.Film, request.Room);

                //a mesma sala não pode passar dois ou mais filmes ao mesmo tempo.
                if (_validationSession.ValidateRoom(session.Room, session.Date, session.StartTime, session.EndTime))
                    return Task.FromResult(new CreateSessionResponse { ErrorMessage = "A Sala esta vinculada a uma sessão neste horário selecionado." });

                _sessionRepository.Create(session);

                return Task.FromResult(new CreateSessionResponse
                {
                    Id = session.Id,
                    Date = session.Date,
                    StartTime = session.StartTime,
                    AnimationType = session.AnimationType,
                    AudioType = session.AudioType,
                    Film = session.Film,
                    Room = session.Room,
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
                return Task.FromResult(new DeleteSessionResponse { ErrorMessage = "Sessão não encontrada na base de dados." });

            //Uma sessão só pode ser removida se faltar 10 dias ou mais para que ela ocorra.
            if (_validationSession.ValidateTimeOfRemove(session.Date))
                return Task.FromResult(new DeleteSessionResponse { ErrorMessage = "A sessão só pode ser removida se faltar 10 dias ou mais para que ela ocorra." });

            _sessionRepository.Delete(session.Id);

            return Task.FromResult(new DeleteSessionResponse { Success = true });
        }
    }
}
