using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EStore.Models.Domain;
using EStore.Repositories.Abstract;

namespace EStore.Controllers
{
	[Authorize]
	public class GenreController : Controller
	{
		private readonly IGenreService genreService;
		public GenreController(IGenreService genreService)
		{
			this.genreService = genreService;
		}
		public IActionResult Add()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Add(Genre model)
		{
			if (!ModelState.IsValid)
				return View(model);
			bool result = genreService.Add(model);
			if (result)
			{
				TempData["msg"] = "Successfully Added";
				return RedirectToAction("Add");
			}
			else
			{
				TempData["msg"] = "Could not add....error on server side /else "+model.GenreName + " already exists";
				return View(model);
			}
		}

		public IActionResult Edit(int id)
		{
			Genre data = genreService.GetById(id);
			return View(data);
		}

		[HttpPost]
		public IActionResult Edit(Genre model)
		{
			if (!ModelState.IsValid)
				return View(model);
			bool result = genreService.Update(model);
			if (result)
			{
				TempData["msg"] = "Successfully Updated";
				return RedirectToAction(nameof(GenreList));
			}
			else
			{
				TempData["msg"] = "Could not update....error on server side";
				return View(model);
			}
		}

		public IActionResult GenreList()
		{
			List<Genre> data = this.genreService.List().ToList();
			return View(data);
		}

		public IActionResult Delete(int id)
		{
			bool result = genreService.Delete(id);
			return RedirectToAction(nameof(GenreList));
		}
	}
}
