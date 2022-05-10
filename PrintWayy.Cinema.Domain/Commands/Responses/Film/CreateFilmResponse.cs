using PrintWayy.Cinema.Domain.Models;

namespace PrintWayy.Cinema.Domain.Commands.Responses.Film
{
    public class CreateFilmResponse:ResultResponse
    {
        public Guid Id { get; set; }
        public ImageData ImageBase64 { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
    }
}
