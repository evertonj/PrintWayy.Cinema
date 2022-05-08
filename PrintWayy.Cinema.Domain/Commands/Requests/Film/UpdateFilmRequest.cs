using MediatR;
using PrintWayy.Cinema.Domain.Commands.Responses.Film;

namespace PrintWayy.Cinema.Domain.Commands.Requests.Film
{
    public class UpdateFilmRequest : IRequest<UpdateFilmResponse>
    {
        public UpdateFilmRequest()
        {

        }

        public UpdateFilmRequest(Guid id, string imagePath, string title, string description, string duration)
        {
            Id = id;
            ImagePath = imagePath;
            Title = title;
            Description = description;
            Duration = duration;
        }

        public Guid Id { get; set; }
        public string ImagePath { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
    }
}
