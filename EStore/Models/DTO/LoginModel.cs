using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EStore.Models.DTO
{
	public class LoginModel
	{
		[Required]
		[DisplayName("User Name")]
		public string? UserName { get; set; }
		[Required]
		public string? Password { get; set; }

	}
}
