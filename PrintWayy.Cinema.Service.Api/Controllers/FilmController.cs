using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrintWayy.Cinema.Domain.Commands.Requests.Film;
using PrintWayy.Cinema.Domain.Interfaces;
using PrintWayy.Cinema.Service.Api.Models;

namespace PrintWayy.Cinema.Service.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiController]
    public class FilmController : ControllerBase
    {
        private readonly IFilmRepository _filmRepository;
        private readonly IMediator _bus;
        public FilmController(IMediator bus, IFilmRepository filmRepository)
        {
            _bus = bus;
            _filmRepository = filmRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            try
            {
                return Ok(_filmRepository.All());
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
                return Ok(_filmRepository.FindById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post(FilmDataModel film)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var request = new CreateFilmRequest(
                    film.ImageBase64,
                    film.Title,
                    film.Description,
                    film.Duration
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


        [HttpPut("{id}")]
        public IActionResult Put(FilmDataModel film)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var request = new UpdateFilmRequest(
                    film.Id,
                    film.ImageBase64,
                    film.Title,
                    film.Description,
                    film.Duration
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
                var request = new DeleteFilmRequest(id);

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
