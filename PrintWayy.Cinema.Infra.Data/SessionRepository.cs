using LiteDB;
using PrintWayy.Cinema.Domain.Interfaces;
using PrintWayy.Cinema.Domain.Models;

namespace PrintWayy.Cinema.Infra.Data
{
    public class SessionRepository : Repository<Session>, ISessionRepository
    {
        public SessionRepository(ILiteDatabase db) : base(db)
        {
        }

        public Session GetSessionByFilm(Film film)
        {
            return Collection.FindOne(session => session.Film.Id == film.Id);
        }

        public Session GetSessionByRoom(Room room)
        {
            return Collection.FindOne(session => session.Room.Name == room.Name);
        }
    }
}
