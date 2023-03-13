using Microsoft.AspNetCore.Mvc;
using EStore.Models.DTO;
using EStore.Repositories.Abstract;

namespace EStore.Controllers
{
	public class UserAuthenticationController:Controller 
	{
		private IUserAuthenticationServices authService;
		public UserAuthenticationController(IUserAuthenticationServices authService)
		{
			this.authService = authService;
		}

		public async Task<IActionResult> Register()
		{
			RegistrationModel model = new RegistrationModel
			{
				Email = "admin@gmail.com",
				UserName = "admin",
				Name = "Admin",
				Password = "Admin@123",
				PasswordConfirm = "Admin@123",
				Role = "Admin"
			};

			Status result = await authService.RegisterAsync(model);
			return Ok(result.Message);
		}

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			Status result = await authService.LoginAsync(model);
			if(result.StatusCode == 1)
			{
				return RedirectToAction("Index", "Home");
			}
			else
			{
				TempData["msg"] = "Could not logged in..";
				return RedirectToAction("Login");
			}
		}

		public async Task<IActionResult> Logout()
		{
			await authService.LogoutAsync();
			return RedirectToAction("Login");
		}
	}
}
