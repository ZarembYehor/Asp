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

        public IActionResult Index(int page = 1)
        {
            var films = repository.Films
                .OrderBy(f => f.Title); 

            var viewModel = new FilmsListViewModel
            {
                Films = films
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize)
                    .ToList(),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = films.Count()
                }
            };

            return View(viewModel);
        }
    }
}
