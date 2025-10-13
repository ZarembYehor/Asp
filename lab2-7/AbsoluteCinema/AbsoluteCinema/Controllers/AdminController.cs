using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AbsoluteCinema.Models;
using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AbsoluteCinema.Controllers
{
    public class AdminController : Controller
    {
        private readonly ICinemaRepository repository;

        public AdminController(ICinemaRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index() => View(repository.Films);

        public ViewResult EditFilm(long filmId) =>
            View("EditFilm", repository.Films.FirstOrDefault(f => f.FilmID == filmId) ?? new Film());

        [HttpPost]
        public IActionResult EditFilm(Film film)
        {
            if (ModelState.IsValid)
            {
                repository.SaveFilm(film);
                TempData["message"] = $"{film.Title} has been saved.";
                return RedirectToAction("Index");
            }
            return View("EditFilm", film);
        }
        public IActionResult FilmDetails(long id)
        {
            var film = repository.Films
                .Include(f => f.Screenings)
                .FirstOrDefault(f => f.FilmID == id);

            if (film == null) return NotFound();
            return View("FilmDetails", film);
        }
        [HttpPost]
        public IActionResult DeleteFilm(long filmId)
        {
            Film? deletedFilm = repository.Films.FirstOrDefault(f => f.FilmID == filmId);
            if (deletedFilm != null)
            {
                repository.DeleteFilm(deletedFilm);
                TempData["message"] = $"{deletedFilm.Title} was deleted.";
            }
            return RedirectToAction("Index");
        }

        public ViewResult Screenings() => View(repository.Screenings);

        public IActionResult ScreeningDetails(long id)
        {
            var screening = repository.Screenings
                .Include(s => s.Film)
                .FirstOrDefault(s => s.ScreeningID == id);

            if (screening == null) return NotFound();
            return View("ScreeningDetails", screening);
        }

        public ViewResult EditScreening(long screeningId)
        {
            ViewBag.Films = new SelectList(repository.Films.OrderBy(f => f.Title), "FilmID", "Title");

            return View("EditScreening",
                repository.Screenings.FirstOrDefault(s => s.ScreeningID == screeningId) ?? new Screening());
        }

        [HttpPost]
        public IActionResult EditScreening(Screening screening)
        {
            ModelState.Remove("Film");

            if (ModelState.IsValid)
            {
                repository.SaveScreening(screening);
                TempData["message"] = $"Screening (Hall {screening.CinemaHall}) has been saved.";
                return RedirectToAction("Screenings");
            }

            var errors = ModelState.Where(x => x.Value?.Errors.Count > 0)
                                    .Select(x => $"{x.Key}: {string.Join("; ", x.Value!.Errors.Select(e => e.ErrorMessage))}")
                                    .ToList();

            if (errors.Any())
            {
                TempData["validation_errors"] = string.Join("<br>", errors);
                TempData["message"] = "Error: Validation failed. Check errors below.";
            }

            ViewBag.Films = new SelectList(repository.Films.OrderBy(f => f.Title), "FilmID", "Title", screening.FilmID);
            return View("EditScreening", screening);
        }

        [HttpPost]
        public IActionResult DeleteScreening(long screeningId)
        {
            Screening? deletedScreening = repository.Screenings.FirstOrDefault(s => s.ScreeningID == screeningId);
            if (deletedScreening != null)
            {
                repository.DeleteScreening(deletedScreening);
                TempData["message"] = $"Screening ID {deletedScreening.ScreeningID} was deleted.";
            }
            return RedirectToAction("Screenings");
        }
    }
}