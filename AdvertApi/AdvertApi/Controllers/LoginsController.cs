using System.Security.Claims;
using Cw11_WebApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvertApi.Controllers
{
	[Route("api/clients/login")]
	[ApiController]

	public class LoginsController : ControllerBase
	{

		private readonly IAdvertDbService _dbService;
		
		public LoginsController(IAdvertDbService dbService)
		{
			_dbService = dbService;
            
		}

		[HttpPost]
        [AllowAnonymous]
        public IActionResult Login(ClientLoginRequest request)
        {
			if (_dbService.CheckLogin(request.Login, request.Password))
			{
				var _token = _dbService.CreateFirstToken(request.Login);
				return Ok(_token);
			}
			else
			{
				return Unauthorized(request.Login + ": login or password is incorrect");
			};
        }

		[HttpPost("refresh-token/{refToken}/")]
		public IActionResult RefreshToken(string refToken)
		{
			var _refToken = _dbService.FindRefreshToken(refToken);
			if (_refToken != null)
			{
				var _newRefToken = _dbService.CreateToken(_refToken);
				return Ok(_newRefToken);
			}
			else
			{
				return Unauthorized("Invalid token");
			};
		}

		
	}
}
