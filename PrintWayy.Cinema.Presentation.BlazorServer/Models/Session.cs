using PrintWayy.Cinema.Domain.Models;
using System.Text.Json.Serialization;

namespace PrintWayy.Cinema.Presentation.BlazorServer.Models
{
    public class Session
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string StartTime { get; set; }
        public AnimationType AnimationType { get; set; }
        public decimal EntryValue { get; set; }
        public AudioType AudioType { get;  set; }
        public Guid FilmId { get; set; }
        public string RoomName { get; set; }
        [JsonIgnore]
        public bool IsDeleting { get; set; } = default!;
    }
}
