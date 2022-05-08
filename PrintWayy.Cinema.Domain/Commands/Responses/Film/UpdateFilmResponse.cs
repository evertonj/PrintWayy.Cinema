namespace PrintWayy.Cinema.Domain.Commands.Responses.Film
{
    public class UpdateFilmResponse:ResultResponse
    {
        public Guid Id { get; set; }
        public string ImagePath { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
    }
}
