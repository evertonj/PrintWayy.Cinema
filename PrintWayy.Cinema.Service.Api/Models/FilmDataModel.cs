using System.ComponentModel.DataAnnotations;

namespace PrintWayy.Cinema.Service.Api.Models
{
    public class FilmDataModel
    {
        [Required]
        public string Title { get;  set; }
        [Required]
        public string Description { get;  set; }
        [Required]
        public string Duration { get; init; } = $"{DateTime.Now:HH:mm:ss}";
        [Required]
        public IFormFile Image { get; set; }
    }
}
