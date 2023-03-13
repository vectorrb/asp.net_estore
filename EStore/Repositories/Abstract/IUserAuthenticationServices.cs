using EStore.Models.DTO;

namespace EStore.Repositories.Abstract
{
	public interface IUserAuthenticationServices
	{
		Task<Status> LoginAsync(LoginModel model);

		Task LogoutAsync();

		Task<Status> RegisterAsync(RegistrationModel model);

	}
}
