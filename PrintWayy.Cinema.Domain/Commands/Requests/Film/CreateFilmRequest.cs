using MediatR;
using PrintWayy.Cinema.Domain.Commands.Responses.Film;

namespace PrintWayy.Cinema.Domain.Commands.Requests.Film
{
    public class CreateFilmRequest : IRequest<CreateFilmResponse>
    {
        public CreateFilmRequest()
        {

        }

        public CreateFilmRequest(string imagePath, string title, string description, string duration)
        {
            ImagePath = imagePath;
            Title = title;
            Description = description;
            Duration = duration;
        }

        public string ImagePath { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
    }
}
