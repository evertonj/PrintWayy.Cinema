using PrintWayy.Cinema.Domain.Models;

namespace PrintWayy.Cinema.Domain.Commands.Responses.Session
{
    public class CreateSessionResponse:ResultResponse
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public decimal EntryValue { get; set; }
        public AnimationType AnimationType { get; set; }
        public AudioType AudioType { get; set; }
        public Models.Film Film { get; set; }
        public Room Room { get; set; }
    }
}
