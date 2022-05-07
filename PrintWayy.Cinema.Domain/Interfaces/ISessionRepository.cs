using PrintWayy.Cinema.Domain.Models;

namespace PrintWayy.Cinema.Domain.Interfaces
{
    public interface ISessionRepository : IRepository<Session>
    {
        Session GetSessionByFilm(Film film);
    }
}
