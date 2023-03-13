using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using EStore.Models.Domain;
using EStore.Repositories.Abstract;
using EStore.Models.Domain;
using EStore.Models.DTO;

namespace EStore.Controllers
{
	[Authorize]
	public class MovieController : Controller
	{
		private readonly IMovieService movieService;
		private readonly IFileService fileService;
		private readonly IGenreService genreService;
		private readonly DBContext dbContext;

		public MovieController(IMovieService movieService, IFileService fileService, 
			IGenreService genreService, DBContext dBContext)
		{
			this.movieService = movieService;
			this.fileService = fileService;
			this.genreService = genreService;
			this.dbContext = dBContext;
		}
		public IActionResult Add()
		{
			Movie model = new Movie();
			model.GenreList = genreService.List().Select(i => new SelectListItem { Text = i.GenreName, Value = i.Id.ToString() });	
			return View(model);
		}
		 
		[HttpPost]
		public IActionResult Add(Movie model)
		 {
			model.GenreList = genreService.List().Select(i => new SelectListItem { Text = i.GenreName, Value = i.Id.ToString() });
			if (!ModelState.IsValid)
				return View(model);
			Tuple<int, string> fileResult = this.fileService.SaveImage(model.ImageFile);
			if (fileResult.Item1 == 0)
			{
				TempData["msg"] = "File could not save";
				return View(model);
			}
			string imageName = fileResult.Item2;
			model.MovieImage = imageName;
			bool result = movieService.Add(model);
			if (result)
			{
				TempData["msg"] = "Successfully Added";
				return RedirectToAction("Add");
			}
			else
			{
				TempData["msg"] = "Could not add....error on server side";
				return View(model);
			}
		}

		public IActionResult Edit(int id)
		{
			Movie model = movieService.GetById(id);
			model.GenreList = genreService.List().Select(i => new SelectListItem { Text = i.GenreName, Value = i.Id.ToString() });
			//List<string> genres = (from genre in dbContext.Genres
			//			  join mg in dbContext.MovieGenres
			//			  on genre.Id equals mg.GenreId
			//			  where mg.MovieId == model.Id
			//			  select genre.GenreName)
			//				  .ToList();

			//string genreNames = string.Join(',', genres);
			//model.GenreNames = genreNames;
			return View(model);
		}

		[HttpPost]
		public IActionResult Edit(Movie model)
		{
			if (!ModelState.IsValid)
				return View(model);
			if(model.ImageFile != null)
			{
				fileService.DeleteImage(model.MovieImage);
				Tuple<int, string> fileResult = this.fileService.SaveImage(model.ImageFile);
				if(fileResult.Item1 == 0)
				{
					TempData["msg"] = "File could not be saved";
					return View(model);
				}
				string imageName = fileResult.Item2;
				model.MovieImage = imageName;

			}
			bool result = movieService.Update(model);
			if (result)
			{
				TempData["msg"] = "Successfully Updated";
				return RedirectToAction(nameof(MovieList));
			}
			else
			{
				TempData["msg"] = "Could not update....error on server side";
				return View(model);
			}
		}

		public IActionResult MovieList(string term = "", int currentPage = 1)
		{
			MovieListVM data = this.movieService.List(term, true, currentPage);
			return View(data);
		}

		public IActionResult Delete(int id)
		{
			bool result = movieService.Delete(id);
			return RedirectToAction(nameof(MovieList));
		}
	}
}
