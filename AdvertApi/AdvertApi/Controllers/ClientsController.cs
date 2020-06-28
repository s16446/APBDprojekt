using AdvertApi.DTO.Requests;
using Cw11_WebApplication.DAL;
using Cw11_WebApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AdvertApi.Controllers
{
	[Route("api/clients")]
	[ApiController]
	public class ClientsController : ControllerBase
	{
        private IConfiguration Configuration { get; set; }

		private readonly IAdvertDbService _dbService;
		
		public ClientsController(IAdvertDbService dbService)
		{
			_dbService = dbService;
		}


		[HttpPost]
		public IActionResult AddClient(ClientRegistrationRequest request)
		{
			try 
			{
				var client = _dbService.AddClient(request);
				return new CreatedAtRouteResult("api/clients", client);
			}
			catch(ClientAlreadyExistException exc)
			{
				return BadRequest(exc.Message);
			}
		}

        [Route("api/clients/login")]
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
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken
                (
                    issuer: "AdvertApi",
                    audience: "Clients",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: creds
                );
            
                var refreshToken = Guid.NewGuid();
                var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
                _dbService.SaveRefreshToken(request.Login, refreshToken.ToString());

                return Ok(new {
                    accessToken,
                    refreshToken
                } );
            }
            else
            {
                return Unauthorized(request.Login + ": login or password is incorrect");
            } ;
        }

		[HttpGet("{login}")]
        [Authorize]
        public IActionResult GetCampaigns(string login)
        {
            var _campaigns = _dbService.GetCampaigns(login);
             
            if (_campaigns != null) {
                return Ok(_campaigns);
            }
            else {
                return NotFound("Login not found");
            }
        }
	}
}
