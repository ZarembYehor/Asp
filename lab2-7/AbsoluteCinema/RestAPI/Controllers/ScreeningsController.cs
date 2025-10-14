using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPI.Models;
using System.Linq;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScreeningsController : ControllerBase
    {
        private readonly ICinemaRepository repository;

        public ScreeningsController(ICinemaRepository repo)
        {
            repository = repo;
        }

        [HttpGet]
        public IActionResult GetScreenings()
        {
            return Ok(repository.Screenings);
        }

        [HttpGet("{id}")]
        public IActionResult GetScreening(long id)
        {
            var screening = repository.Screenings
                .Include(s => s.Film)
                .FirstOrDefault(s => s.ScreeningID == id);

            if (screening == null) return NotFound();
            return Ok(screening);
        }

        [HttpPost]
        public IActionResult CreateScreening([FromBody] Screening screening)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            repository.CreateScreening(screening);
            return CreatedAtAction(nameof(GetScreening), new { id = screening.ScreeningID }, screening);
        }

        [HttpPut]
        public IActionResult UpdateScreening([FromBody] Screening screening)
        {
            if (screening.ScreeningID == 0 || !ModelState.IsValid) return BadRequest(ModelState);
            repository.SaveScreening(screening);
            return Ok(screening);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteScreening(long id)
        {
            Screening? screening = repository.Screenings.FirstOrDefault(s => s.ScreeningID == id);
            if (screening == null) return NotFound();

            repository.DeleteScreening(screening);
            return NoContent();
        }
    }
}