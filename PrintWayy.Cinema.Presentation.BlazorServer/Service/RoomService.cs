using PrintWayy.Cinema.Domain.Models;

namespace PrintWayy.Cinema.Presentation.BlazorServer.Service
{
    public class RoomService
    {
        public static Task<Room[]> GetRoomsAsync()
        {
            return Task.FromResult(Room.GetRooms().ToArray());
        }
    }
}
