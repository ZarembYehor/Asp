using Microsoft.AspNetCore.Mvc;
using AbsoluteCinema.Models;
using AbsoluteCinema.Models.ViewModels;
using System.Linq;

namespace AbsoluteCinema.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICinemaRepository repository;
        private const int PageSize = 3;

        public HomeController(ICinemaRepository repo)
        {
            repository = repo;
        }

        public IActionResult Index(string? genre, int page = 1)
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


            return View(viewModel);
        }
    }
}