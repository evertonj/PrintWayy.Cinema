using LiteDB;
using PrintWayy.Cinema.Domain.Interfaces;
using PrintWayy.Cinema.Domain.Models;

namespace PrintWayy.Cinema.Infra.Data
{
    public class FilmRepository : Repository<Film>, IFilmRepository
    {
        public FilmRepository(ILiteDatabase db) : base(db)
        {
        }

        public Film GetFilmByTitle(string title)
        {
            return Collection.FindOne(film => film.Title == title);
        }
    }
}
