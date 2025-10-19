using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPI.Models;
using RestAPI.Models.ViewModels;
using System.Linq;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmsController : ControllerBase
    {
        private readonly ICinemaRepository repository;
        private const int PageSize = 3;

        public FilmsController(ICinemaRepository repo)
        {
            repository = repo;
        }

        [HttpGet]
        public IActionResult GetFilms(string? genre, int page = 1)
        {
            var filmsQuery = repository.Films
                .Where(f => genre == null || f.Genre == genre)
                .OrderBy(f => f.Title);

            var viewModel = new FilmsListViewModel
            {
                Films = filmsQuery
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize)
                    .ToList(),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = filmsQuery.Count()
                },
                CurrentGenre = genre
            };
            return Ok(viewModel);
        }

        [HttpGet("{id}")]
        public IActionResult GetFilm(long id)
        {
            var film = repository.Films
                .Include(f => f.Screenings)
                .FirstOrDefault(f => f.FilmID == id);

            if (film == null) return NotFound();
            return Ok(film);
        }

        [HttpPost]
        public IActionResult CreateFilm([FromBody] Film film)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            repository.CreateFilm(film);
            return CreatedAtAction(nameof(GetFilm), new { id = film.FilmID }, film);
        }
        [HttpPut]
        public IActionResult UpdateFilm([FromBody] Film film)
        {
            if (film.FilmID == 0 || !ModelState.IsValid) return BadRequest(ModelState);
            repository.SaveFilm(film);
            return Ok(film);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFilm(long id)
        {
            Film? film = repository.Films.FirstOrDefault(f => f.FilmID == id);
            if (film == null) return NotFound();

            repository.DeleteFilm(film);
            return NoContent(); 
        }
    }
}