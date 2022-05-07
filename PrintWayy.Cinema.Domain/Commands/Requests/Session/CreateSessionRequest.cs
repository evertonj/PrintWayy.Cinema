using MediatR;
using PrintWayy.Cinema.Domain.Commands.Responses.Session;
using PrintWayy.Cinema.Domain.Models;

namespace PrintWayy.Cinema.Domain.Commands.Requests.Session
{
    public class CreateSessionRequest: IRequest<CreateSessionResponse>
    {
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public decimal EntryValue { get; set; }
        public AnimationType AnimationType { get; set; }
        public AudioType AudioType { get; set; }
        public Models.Film Film { get; set; }
        public Room Room { get; set; }
    }
}
