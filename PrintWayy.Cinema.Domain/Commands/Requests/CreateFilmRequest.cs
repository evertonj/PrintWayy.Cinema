using MediatR;
using PrintWayy.Cinema.Domain.Responses;

namespace PrintWayy.Cinema.Domain.Requests
{
    public class CreateFilmRequest:IRequest<CreateFilmResponse>
    {
        public string ImagePath { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
