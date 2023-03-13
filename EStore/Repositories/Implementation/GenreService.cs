using Microsoft.AspNetCore.Identity;
using EStore.Models.Domain;
using EStore.Models.DTO;
using EStore.Repositories.Abstract;
using System.Security.Claims;

namespace EStore.Repositories.Implementation
{
	public class GenreService : IGenreService
	{
		private readonly DBContext dbContext;
		public GenreService(DBContext dBContext)
		{
			this.dbContext = dBContext;
		}
		public bool Add(Genre model)
		{
			try
			{
				Genre data = dbContext.Genres.FirstOrDefault(g => g.GenreName == model.GenreName);
				if(data == null)
				{
					dbContext.Genres.Add(model);
					dbContext.SaveChanges();
					return true;
				}
				else
				{
					return false;
				}
				
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
				Genre data = this.GetById(id);
				if(data == null)
					return false;
				dbContext.Genres.Remove(data);
				dbContext.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{

				return false;
			}
		}

		public Genre GetById(int id)
		{
			return dbContext.Genres.Find(id);
		}

		public IQueryable<Genre> List()
		{
			IQueryable<Genre> data = dbContext.Genres.AsQueryable();
			return data;
		}

		public bool Update(Genre model)
		{
			try
			{
				dbContext.Genres.Update(model);
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
