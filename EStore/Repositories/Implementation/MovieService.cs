using Microsoft.AspNetCore.Identity;
using EStore.Models.Domain;
using EStore.Models.DTO;
using EStore.Repositories.Abstract;
using System.Security.Claims;

namespace EStore.Repositories.Implementation
{
	public class MovieService : IMovieService
	{
		private readonly DBContext dbContext;
		private readonly IFileService fileService;
		public MovieService(DBContext dBContext, IFileService fileService)
		{
			this.dbContext = dBContext;
			this.fileService = fileService;
		}
		public bool Add(Movie model)
		{
			try
			{
				dbContext.Movies.Add(model);
				dbContext.SaveChanges();
				foreach (int genreId in model.Genres)
				{
					MovieGenre movieGenre = new MovieGenre
					{
						MovieId = model.Id,
						GenreId = genreId
					};
					dbContext.MovieGenres.Add(movieGenre);
				}
				dbContext.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{

				return false;
			}
		}

		public bool Delete(int id)
		{
			try
			{
				Movie data = this.GetById(id);
				if (data == null)
					return false;
				IQueryable<MovieGenre> MovieGenres = dbContext.MovieGenres.Where(a => a.MovieId == data.Id);
				foreach(MovieGenre movieGenre in MovieGenres)
				{
					dbContext.MovieGenres.Remove(movieGenre);
				}
				fileService.DeleteImage(data.MovieImage);
				dbContext.Movies.Remove(data);
				dbContext.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{

				return false;
			}
		}

		public Movie GetById(int id)
		{
			return dbContext.Movies.Find(id);
		}

		public MovieListVM List(string term= "", bool paging=false, int currentPage=0 )
		{
			MovieListVM data = new MovieListVM();
			List<Movie> list = dbContext.Movies.ToList();
			if (!string.IsNullOrEmpty(term))
			{
				term = term.ToLower();
				list = list.Where(a => a.Title.ToLower().StartsWith(term)).ToList();
			}
			if (paging)
			{
				int pageSize = 4;
				int count = list.Count;
				int TotalPages = (int)Math.Ceiling(count / (double)pageSize);
				list = list.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
				data.PageSize = pageSize;
				data.CurrentPage = currentPage;
				data.TotalPages = TotalPages;
			}
			
			foreach (Movie movie in list)
			{
				List<string> genres = (from genre in dbContext.Genres
							  join mg in dbContext.MovieGenres
							  on genre.Id equals mg.GenreId
							  where mg.MovieId == movie.Id
							  select genre.GenreName)
							  .ToList();

				string genreNames = string.Join(',', genres);
				movie.GenreNames = genreNames;
			}
			data.MovieList = list.AsQueryable();
			return data;
		}

		public bool Update(Movie model)
		{
			try
			{
				List<MovieGenre> genresToDeleted = dbContext.MovieGenres.Where(a => a.MovieId == model.Id && !model.Genres.Contains(a.GenreId)).ToList();
				foreach (MovieGenre mGenre in genresToDeleted)
				{
					dbContext.MovieGenres.Remove(mGenre);
				}
				foreach (int genId in model.Genres)
				{
					MovieGenre movieGenre = dbContext.MovieGenres.FirstOrDefault(a => a.MovieId == model.Id && a.GenreId == genId);
					if (movieGenre == null)
					{
						movieGenre = new MovieGenre { GenreId = genId, MovieId = model.Id };
						dbContext.MovieGenres.Add(movieGenre);
					}
				}

				dbContext.Movies.Update(model);
				// we have to add these genre ids in movieGenre table
				dbContext.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{

				return false;
			}
		}
	}
}
