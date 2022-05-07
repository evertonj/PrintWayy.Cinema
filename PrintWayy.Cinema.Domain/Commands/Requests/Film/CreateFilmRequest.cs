using MediatR;
using PrintWayy.Cinema.Domain.Commands.Responses.Film;

namespace PrintWayy.Cinema.Domain.Commands.Requests.Film
{
    public class CreateFilmRequest : IRequest<CreateFilmResponse>
    {
        public string ImagePath { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
