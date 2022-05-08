using MediatR;
using Microsoft.AspNetCore.Mvc;
using PrintWayy.Cinema.Domain.Commands.Requests.Session;
using PrintWayy.Cinema.Domain.Interfaces;
using PrintWayy.Cinema.Service.Api.Models;

namespace PrintWayy.Cinema.Service.Api.Controllers
{
    [Route("api/v1/[controller]")]
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
        public IActionResult Get()
        {
            try
            {
                return Ok(_sessionRepository.All());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
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
        public IActionResult Post([FromForm] SessionDataModel session)
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
