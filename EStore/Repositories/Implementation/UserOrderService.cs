using EStore.Models.Domain;
using Microsoft.AspNetCore.Identity;
using EStore.Models.Domain;
using EStore.Repositories.Abstract;

namespace EStore.Repositories.Implementation
{
	public class UserOrderService : IUserOrderService
	{
		private readonly UserManager<ApplicationUser> userManager;
		//private readonly RoleManager<IdentityRole> roleManager;
		//private readonly SignInManager<ApplicationUser> signInManager;
		private readonly DBContext dbContext;
		private readonly IHttpContextAccessor httpContextAccessor;

		public UserOrderService(UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager,
			DBContext dBContext, IHttpContextAccessor httpContextAccessor)
		{
			this.userManager = userManager;
			//this.roleManager = roleManager;
			//this.signInManager = signInManager;
			this.dbContext = dBContext;
			this.httpContextAccessor = httpContextAccessor;

		}
		public Task<IEnumerable<Order>> UserOrders()
		{

		}
	}
}
