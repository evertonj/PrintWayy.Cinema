using PrintWayy.Cinema.Presentation.BlazorServer.Models;
using PrintWayy.Cinema.Presentation.BlazorServer.Service.Interfaces;

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
            return Task.FromResult(_httpService.Get<IEnumerable<Session>>("api/v1/Session"));
        }

        public async Task RemoveSession(Guid id)
        {
            await _httpService.Delete<Session>($"api/v1/Session/{id}");
        }
    }
}
