using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AdvertApi.DTO.Requests;
using Cw11_WebApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AdvertApi.Controllers
{
	[Route("api/clients/login")]
	[ApiController]

	public class LoginsController : ControllerBase
	{
        private IConfiguration Configuration { get; set; }

		private readonly IAdvertDbService _dbService;
		
		public LoginsController(IAdvertDbService dbService, IConfiguration configuration)
		{
			_dbService = dbService;
            Configuration = configuration;
		}

		[HttpPost]
        [AllowAnonymous]
        public IActionResult Login(ClientLoginRequest request)
        {
            var claims = new[]
			{
			    new Claim(ClaimTypes.NameIdentifier, request.Login),
                new Claim(ClaimTypes.Hash, request.Password),
			};

			if (_dbService.CheckLogin(request.Login, request.Password))
			{
				var _client = _dbService.GetClient(request.Login);
				var _token = _dbService.CreateToken(_client.refreshToken);
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
