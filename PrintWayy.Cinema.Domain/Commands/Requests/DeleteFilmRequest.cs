using MediatR;
using PrintWayy.Cinema.Domain.Commands.Responses;

namespace PrintWayy.Cinema.Domain.Commands.Requests
{
    public class DeleteFilmRequest:IRequest<DeleteFilmResponse>
    {
        public Guid Id { get; set; }
    }
}
