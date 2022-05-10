using PrintWayy.Cinema.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace PrintWayy.Cinema.Service.Api.Models
{
    public class FilmDataModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get;  set; }
        [Required]
        public string Description { get;  set; }
        [Required]
        public string Duration { get; init; } = $"{DateTime.Now:HH:mm:ss}";
        [Required]
        public ImageData ImageBase64 { get; set; }
    }
}
