using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrintWayy.Cinema.Domain.Interfaces;
using PrintWayy.Cinema.Domain.Models;
using PrintWayy.Cinema.Service.Api.Services;

namespace PrintWayy.Cinema.Service.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public IActionResult Authenticate(User user)
        {
            var userAuthenticate = _userRepository.Authenticate(user);

            if (userAuthenticate == null)
                return NotFound("Usuário ou senha inválidos");

            var token = TokenService.GenerateToken(userAuthenticate);
            user.Password = string.Empty;
            return Ok(new AuthenticateData { User = userAuthenticate, Token = token });
        }
    }
}
