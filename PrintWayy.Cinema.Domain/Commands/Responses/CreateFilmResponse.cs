namespace PrintWayy.Cinema.Domain.Responses
{
    public class CreateFilmResponse
    {
        public Guid Id { get; set; }
        public string ImagePath { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
