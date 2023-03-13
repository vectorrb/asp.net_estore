using EStore.Models.Domain;
using EStore.Models.DTO;

namespace EStore.Repositories.Abstract
{
	public interface IGenreService
	{
		bool Add(Genre model);
		bool Update(Genre model);
		Genre GetById(int id);
		bool Delete(int id);
		IQueryable<Genre> List();
	}
}
