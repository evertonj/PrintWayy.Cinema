using PrintWayy.Cinema.Domain.Commands.Requests.Film;
using PrintWayy.Cinema.Domain.Commands.Requests.Session;
using PrintWayy.Cinema.Domain.Models;
using System;

namespace PrintWayy.Cinema.Domain.Test
{
    public class ObjectMother
    {
        public static Film FilmObject
        {
            get
            {
                return new Film
                (
                     "great-place-to-work-pw.png",
                     "Test",
                     "Test 2",
                     new TimeSpan(2, 29, 18)
                );
            }
        }
        public static CreateSessionRequest CreateSessionRequestObject
        {
            get
            {
                return new CreateSessionRequest
                {
                    AnimationType = AnimationType.TwoDimensions,
                    AudioType = AudioType.Original,
                    Date = DateTime.Now.AddDays(1),
                    StartTime = new TimeSpan(22, 30, 00).ToString(Film.DURATION_PATTERN),
                    EntryValue = new decimal(50.59),
                    FilmId = FilmObject.Id,
                    RoomName = Room.GetRooms()[0].Name
                };
            }
        }

        public static CreateFilmRequest CreateFilmRequestObject
        {
            get
            {
                return new CreateFilmRequest() { ImagePath = "great-place-to-work-pw.png", Title = "O jogo da imitação", Description = "A História do pai da informática.", Duration = new System.TimeSpan(1, 54, 0).ToString(Film.DURATION_PATTERN) };
            }
        } 
    }
}
