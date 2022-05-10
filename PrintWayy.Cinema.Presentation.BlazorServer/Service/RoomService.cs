using PrintWayy.Cinema.Domain.Models;
using PrintWayy.Cinema.Presentation.BlazorServer.Service.Interfaces;

namespace PrintWayy.Cinema.Presentation.BlazorServer.Service
{
    public class RoomService : IRoomService
    {
        public Task<List<Room>> GetAll()
        {
            return Task.FromResult(Room.GetRooms());
        }
    }
}
