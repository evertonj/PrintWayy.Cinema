using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrintWayy.Cinema.Domain.Commands.Requests.Session;
using PrintWayy.Cinema.Domain.Interfaces;
using PrintWayy.Cinema.Domain.Models;
using PrintWayy.Cinema.Service.Api.Models;

namespace PrintWayy.Cinema.Service.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IMediator _bus;
        public SessionController(IMediator bus, ISessionRepository sessionRepository)
        {
            _bus = bus;
            _sessionRepository = sessionRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            try
            {
                var listOfSessionResult = new List<SessionDataModel>();
                foreach (var session in _sessionRepository.All())
                {
                    var sessionResult = new SessionDataModel
                    {
                        AnimationType = session.AnimationType,
                        AudioType = session.AudioType,
                        Date = session.Date,
                        EntryValue = session.EntryValue,
                        FilmId = session.Film.Id,
                        Id = session.Id,
                        RoomName = session.Room.Name,
                        StartTime = session.StartTime.ToString(Film.DURATION_PATTERN)
                    };
                    listOfSessionResult.Add(sessionResult);
                }
                return Ok(listOfSessionResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(Guid id)
        {
            try
            {
                return Ok(_sessionRepository.FindById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post(SessionDataModel session)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var request = new CreateSessionRequest(
               session.Date,
               session.StartTime,
               session.EntryValue,
               session.AnimationType,
               session.AudioType,
               session.FilmId,
               session.RoomName
               );

                var response = _bus.Send(request).Result;

                if (response.Success)
                    return Ok(response);
                else
                    return BadRequest(response.ErrorMessage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var request = new DeleteSessionRequest(id);

                var response = _bus.Send(request).Result;

                if (response.Success)
                    return Ok(response);
                else
                    return BadRequest(response.ErrorMessage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
