using System.Text.Json;

namespace PrintWayy.Cinema.Domain.Models
{
    public class Room
    {
        public string Name { get; set; }
        public uint NumberSeats { get; set; }

        public static List<Room> GetRooms()
        {
            try
            {
                var content = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "Rooms.json");
                return JsonSerializer.Deserialize<List<Room>>(content);
            }
            catch
            {
                return new List<Room> { new Room { Name = "Sala 1", NumberSeats = 40 }, new Room { Name = "Sala 2", NumberSeats = 50 }, new Room { Name = "Sala 3", NumberSeats = 60 } };
            }
        }
    }
}
