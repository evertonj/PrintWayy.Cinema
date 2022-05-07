using MediatR;
using PrintWayy.Cinema.Domain.Commands.Responses.Session;

namespace PrintWayy.Cinema.Domain.Commands.Requests.Session
{
    public class DeleteSessionRequest:IRequest<DeleteSessionResponse>
    {
        public Guid Id { get; set; }
    }
}
