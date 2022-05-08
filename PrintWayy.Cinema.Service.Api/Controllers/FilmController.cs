using MediatR;
using Microsoft.AspNetCore.Mvc;
using PrintWayy.Cinema.Domain.Commands.Requests.Film;
using PrintWayy.Cinema.Domain.Interfaces;
using PrintWayy.Cinema.Service.Api.Models;
using Microsoft.AspNetCore.Hosting;

namespace PrintWayy.Cinema.Service.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FilmController : ControllerBase
    {
        private readonly IFilmRepository _filmRepository;
        private readonly IMediator _bus;
        private readonly IWebHostEnvironment _env;
        public FilmController(IMediator bus, IFilmRepository filmRepository, IWebHostEnvironment env)
        {
            _bus = bus;
            _filmRepository = filmRepository;
            _env = env;
        }

        [HttpGet]
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
        public IActionResult Post([FromForm]FilmDataModel film)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                string fileLocation = SaveImage(film);

                var request = new CreateFilmRequest(
                    fileLocation,
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
        public IActionResult Put(Guid id, [FromForm] FilmDataModel film)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                string fileLocation = SaveImage(film);

                var request = new UpdateFilmRequest(
                    id,
                    fileLocation,
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

        private string SaveImage(FilmDataModel film)
        {
            //Get the complete folder path for storing the profile image inside it.  
            var path = Path.Combine(_env.ContentRootPath, "FilmImages/");

            //checking if "images" folder exist or not exist then create it
            if ((!Directory.Exists(path)))
            {
                Directory.CreateDirectory(path);
            }
            //getting file name and combine with path and save it
            string filename = film.Image.FileName;
            using (var fileStream = new FileStream(Path.Combine(path, filename), FileMode.Create))
            {
                film.Image.CopyTo(fileStream);
            }
            //save folder path 
            var fileLocation = "FilmImages/" + filename;
            return fileLocation;
        }
    }
}
