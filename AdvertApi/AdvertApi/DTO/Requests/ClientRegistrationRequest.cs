using System.ComponentModel.DataAnnotations;

namespace AdvertApi.DTO.Requests
{
	public class ClientRegistrationRequest
	{
		public string FirstName { get; set; }

		public string LastName { get; set; }

		[RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
		public string Email { get; set; }

		[RegularExpression("^[0-9]{3}-[0-9]{3}-[0-9]{3}")]  
		public string Phone { get; set; }

		public string Login { get; set; }

		public string Password { get; set; }

	}
}
