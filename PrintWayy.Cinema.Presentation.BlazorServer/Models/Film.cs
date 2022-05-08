using System.Text.Json.Serialization;

namespace PrintWayy.Cinema.Presentation.BlazorServer.Models
{
    public class Film
    {
        public Guid Id { get; set; }
        public string ImagePath { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        [JsonIgnore]
        public bool IsDeleting { get; set; } = default!;
    }
}
