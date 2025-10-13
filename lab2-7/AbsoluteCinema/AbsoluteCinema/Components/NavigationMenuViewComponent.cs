using Microsoft.AspNetCore.Mvc;
using AbsoluteCinema.Models;
using System.Linq;

namespace AbsoluteCinema.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly ICinemaRepository repository;

        public NavigationMenuViewComponent(ICinemaRepository repo)
        {
            repository = repo;
        }

        public IViewComponentResult Invoke()
        {
            string? genre = RouteData.Values["genre"] as string;

            ViewBag.SelectedGenre = genre;

            return View(repository.Films
                .Select(f => f.Genre)
                .Distinct()
                .OrderBy(g => g));
        }
    }
}