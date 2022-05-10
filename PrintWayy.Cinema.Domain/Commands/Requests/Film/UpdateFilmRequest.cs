using MediatR;
using PrintWayy.Cinema.Domain.Commands.Responses.Film;
using PrintWayy.Cinema.Domain.Models;

namespace PrintWayy.Cinema.Domain.Commands.Requests.Film
{
    public class UpdateFilmRequest : IRequest<UpdateFilmResponse>
    {
        public UpdateFilmRequest()
        {

        }

        public UpdateFilmRequest(Guid id, ImageData imageBase64, string title, string description, string duration)
        {
            Id = id;
            ImageBase64 = imageBase64;
            Title = title;
            Description = description;
            Duration = duration;
        }

        public Guid Id { get; set; }
        public ImageData ImageBase64 { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
    }
}
