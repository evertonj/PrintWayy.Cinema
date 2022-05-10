using PrintWayy.Cinema.Presentation.BlazorServer.Models;

namespace PrintWayy.Cinema.Presentation.BlazorServer.Service.Interfaces
{
    public interface ISessionService
    {
        Task AddSession(Session session);
        Task RemoveSession(Guid id);
        Task<IEnumerable<Session>> GetAll();
    }
}
