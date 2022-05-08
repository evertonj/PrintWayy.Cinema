using PrintWayy.Cinema.Presentation.BlazorServer.Models;
using PrintWayy.Cinema.Presentation.BlazorServer.Service.Interfaces;
using PrintWayy.Cinema.Presentation.BlazorServer.Shared;

namespace PrintWayy.Cinema.Presentation.BlazorServer.Service
{
    public class FilmService : IFilmService
    {
        private IHttpService _httpService;

        public FilmService(IHttpService httpService)
        {
            _httpService = httpService;
        }
        public async Task AddFilmViewModel(Film film)
        {
            await _httpService.Post($"api/v1/Film", film);
        }

        public async Task DeleteFilmViewModel(Guid id)
        {
            await _httpService.Delete($"api/v1/Film/{id}");
        }

        public async Task<Film> GetFilmViewModel(Guid id)
        {
            return await _httpService.Get<Film>($"api/v1/Film/{id}");
        }

        public async Task UpdateFilmViewModel(Film film)
        {
            await _httpService.Put($"api/v1/Film", film);
        }
        
        public async Task<IEnumerable<Film>> GetAll()
        {
            var result = await _httpService.Get<IEnumerable<Film>>("api/v1/Film");
            return result;
        }

        public async Task<PagedResult<Film>> GetFilm(string name, string page)
        {
            int pageSize = 5;

            if (name != null)
            {
                var result = GetAll().Result;
                var paged = result.Where(p => p.Title.Contains(name, StringComparison.CurrentCultureIgnoreCase) ||
                        p.Title.Contains(name, StringComparison.CurrentCultureIgnoreCase))
                    .GetPaged(int.Parse(page), pageSize);
                return await Task.FromResult(paged);
            }
            else
            {
                var result = GetAll().Result;
                var paged = result.GetPaged(int.Parse(page), pageSize);
                return await Task.FromResult(paged);
            }
        }
    }
}
