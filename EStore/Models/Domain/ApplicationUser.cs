using Microsoft.AspNetCore.Identity;

namespace EStore.Models.Domain
{
	public class ApplicationUser: IdentityUser
	{
		public string Name { get; set; }
	}
}
