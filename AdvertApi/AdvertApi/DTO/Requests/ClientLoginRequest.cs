using System.ComponentModel.DataAnnotations;

namespace AdvertApi.Controllers
{
	public class ClientLoginRequest
	{
		public string Login { get; set; }

		[Required]
		public string Password { get; set; }
	}
}