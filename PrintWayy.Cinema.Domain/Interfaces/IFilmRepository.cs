using PrintWayy.Cinema.Domain.Models;

namespace PrintWayy.Cinema.Domain.Interfaces
{
    public interface IFilmRepository : IRepository<Film>
    {
        Film GetFilmByTitle(string title);
    }
}
