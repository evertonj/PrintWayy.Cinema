using MediatR;
using PrintWayy.Cinema.Domain.Commands.Responses.Session;

namespace PrintWayy.Cinema.Domain.Commands.Requests.Session
{
    public class DeleteSessionRequest:IRequest<DeleteSessionResponse>
    {
        public DeleteSessionRequest()
        {

        }
        public DeleteSessionRequest(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
