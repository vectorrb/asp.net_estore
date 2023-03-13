using Microsoft.AspNetCore.Mvc;
using EStore.Models.Domain;
using EStore.Models.DTO;
using EStore.Repositories.Abstract;

namespace EStore.Controllers
{
	public class HomeController : Controller
	{
		private readonly IMovieService movieService;
		public HomeController(IMovieService movieService)
		{
			this.movieService = movieService;
		}
		public IActionResult Index(string term="", int currentPage = 1)
		{
			MovieListVM movies = movieService.List(term, true, currentPage);
			return View(movies);
		}

		public IActionResult About()
		{
			return View();
		}
		public IActionResult MovieDetail(int movieId)
		{
			Movie movie = movieService.GetById(movieId);
			return View(movie);
		}
	}
}
