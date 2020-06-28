using System;

namespace AdvertApi.Controllers
{
	public class Token
	{
		public string AccessToken { get; set; }

		public Guid RefreshToken { get; set; }
	}
}