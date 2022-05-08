using PrintWayy.Cinema.Presentation.BlazorServer.Models;
using PrintWayy.Cinema.Presentation.BlazorServer.Shared;

namespace PrintWayy.Cinema.Presentation.BlazorServer.Service.Interfaces
{
    public interface IFilmService
    {
        Task<Film> GetFilmViewModel(Guid id);

        Task DeleteFilmViewModel(Guid id);

        Task AddFilmViewModel(Film film);

        Task UpdateFilmViewModel(Film film);
        Task<IEnumerable<Film>> GetAll();
        Task<PagedResult<Film>> GetFilm(string name, string page);
    }
}
