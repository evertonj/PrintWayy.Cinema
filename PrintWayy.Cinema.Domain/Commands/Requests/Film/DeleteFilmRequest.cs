using MediatR;
using PrintWayy.Cinema.Domain.Commands.Responses.Film;

namespace PrintWayy.Cinema.Domain.Commands.Requests.Film
{
    public class DeleteFilmRequest : IRequest<DeleteFilmResponse>
    {
        public DeleteFilmRequest()
        {

        }
        public DeleteFilmRequest(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
