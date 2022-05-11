using PrintWayy.Cinema.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace PrintWayy.Cinema.Service.Api.Models
{
    public class SessionDataModel
    {
        public Guid Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string StartTime { get;  set; }
        [Required]
        public decimal EntryValue { get;  set; }
        [Required]
        public AnimationType AnimationType { get;  set; }
        [Required]
        public AudioType AudioType { get;  set; }
        [Required]
        public Guid FilmId { get;  set; }
        [Required]
        public string RoomName { get;  set; }
    }
}
