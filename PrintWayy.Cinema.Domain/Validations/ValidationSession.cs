using PrintWayy.Cinema.Domain.Interfaces;
using PrintWayy.Cinema.Domain.Models;

namespace PrintWayy.Cinema.Domain.Validations
{
    public class ValidationSession
    {
        private readonly ISessionRepository _sessionRepository;

        public ValidationSession(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public bool ValidateRoom(Room room, DateTime date, TimeSpan startTime, TimeSpan endTime)
        {
            var sessions = _sessionRepository.All();
            foreach (var session in sessions)
            {
                var sameRoom = session.Room.Name == room.Name;
                var sameDate = session.Date.ToString("d") == date.ToString("d");
                var betweenStartTime = startTime >= session.StartTime && startTime <= session.EndTime;
                var betweenEndTime = endTime >= session.StartTime && endTime <= session.EndTime;
                if (sameRoom && sameDate && betweenStartTime && betweenEndTime)
                {
                    return true;
                }
            }
            return false;
        }

        public bool ValidateTimeOfRemove(DateTime date)
        {
            return DateTime.Now.AddDays(10) > date;
        }
    }
}
