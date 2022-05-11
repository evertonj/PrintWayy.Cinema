using PrintWayy.Cinema.Presentation.BlazorServer.Models;
using PrintWayy.Cinema.Presentation.BlazorServer.Service.Interfaces;
using PrintWayy.Cinema.Presentation.BlazorServer.Shared;

namespace PrintWayy.Cinema.Presentation.BlazorServer.Service
{
    public class SessionService : ISessionService
    {
        private readonly IHttpService _httpService;

        public SessionService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task AddSession(Session session)
        {
            await _httpService.Post<Session>("api/v1/Session",session);
        }

        public Task<IEnumerable<Session>> GetAll()
        {
            return _httpService.Get<IEnumerable<Session>>("api/v1/Session");
        }

        public async Task RemoveSession(Guid id)
        {
            await _httpService.Delete<Session>($"api/v1/Session/{id}");
        }

        public async Task<PagedResult<Session>> GetSessionByDay(DateTime? day, string page, IEnumerable<Session> sessions)
        {
            int pageSize = 5;

            if (day != null)
            {
                var paged = sessions.Where(p => p.Date.ToShortDateString() == day.Value.ToShortDateString())
                    .GetPaged(int.Parse(page), pageSize);
                return await Task.FromResult(paged);
            }
            else
            {
                var paged = sessions.GetPaged(int.Parse(page), pageSize);
                return await Task.FromResult(paged);
            }
        }
    }
}
