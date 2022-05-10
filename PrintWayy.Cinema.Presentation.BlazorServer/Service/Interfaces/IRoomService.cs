using PrintWayy.Cinema.Domain.Models;

namespace PrintWayy.Cinema.Presentation.BlazorServer.Service.Interfaces
{
    public interface IRoomService
    {
        Task<List<Room>> GetAll();
    }
}