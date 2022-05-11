using System.ComponentModel.DataAnnotations;

namespace PrintWayy.Cinema.Presentation.BlazorServer.Models
{
    public class Login
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
