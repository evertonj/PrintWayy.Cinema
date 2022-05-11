using PrintWayy.Cinema.Presentation.BlazorServer.Models;
using PrintWayy.Cinema.Presentation.BlazorServer.Shared;

namespace PrintWayy.Cinema.Presentation.BlazorServer.Service.Interfaces
{
    public interface ISessionService
    {
        Task AddSession(Session session);
        Task RemoveSession(Guid id);
        Task<IEnumerable<Session>> GetAll();
        Task<PagedResult<Session>> GetSessionByDay(DateTime? day, string page, IEnumerable<Session> sessions);
    }
}
