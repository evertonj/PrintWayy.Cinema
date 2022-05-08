using MediatR;
using PrintWayy.Cinema.Domain.Commands.Responses.Session;
using PrintWayy.Cinema.Domain.Models;

namespace PrintWayy.Cinema.Domain.Commands.Requests.Session
{
    public class CreateSessionRequest: IRequest<CreateSessionResponse>
    {
        public CreateSessionRequest() { }
        public CreateSessionRequest(DateTime date, string startTime, decimal entryValue, AnimationType animationType, AudioType audioType, Guid filmId, string roomName)
        {
            Date = date;
            StartTime = startTime;
            EntryValue = entryValue;
            AnimationType = animationType;
            AudioType = audioType;
            FilmId = filmId;
            RoomName = roomName;
        }

        public DateTime Date { get; set; }
        public string StartTime { get; set; }
        public decimal EntryValue { get; set; }
        public AnimationType AnimationType { get; set; }
        public AudioType AudioType { get; set; }
        public Guid FilmId { get; set; }
        public string RoomName { get; set; }
    }
}
