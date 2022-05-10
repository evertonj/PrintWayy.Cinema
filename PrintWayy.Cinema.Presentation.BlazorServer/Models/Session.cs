using PrintWayy.Cinema.Domain.Models;

namespace PrintWayy.Cinema.Presentation.BlazorServer.Models
{
    public class Session
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string StartTime { get; set; }
        public AnimationType AnimationType { get; set; }
        public decimal EntryValue { get; set; }
        public AudioType AudioType { get;  set; }
        public Guid FilmId { get; set; }
        public string RoomName { get; set; }
    }
}
