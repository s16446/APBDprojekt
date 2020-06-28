using AdvertApi.Controllers;
using AdvertApi.DTO.Requests;
using AdvertApi.Models;
using Cw11_WebApplication.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Cw11_WebApplication.DAL
{
	public class AdvertDbService : IAdvertDbService
	{
		private readonly AdvertDbContext _context;
		private IConfiguration Configuration { get; set; }

		public AdvertDbService(AdvertDbContext context, IConfiguration configuration)
        {
           _context = context;
		   Configuration = configuration;
        }

		public Client AddClient(ClientRegistrationRequest request)
		{
            var _salt = Password.CreateSalt();
            var _passwordHash = Password.CreatePasswordHash(request.Password, _salt);

			if (_context.Clients.FirstOrDefault( c => c.Login == request.Login) != null)
				throw new ClientAlreadyExistException($"Client with login = {request.Login} already exists");
				
			var _client = _context.Clients.Add(new Client{
				FirstName = request.FirstName,
				LastName = request.LastName,
				Email = request.Email,
				Phone = request.Phone,
				Login = request.Login,
				Password = _passwordHash,
                Salt = _salt
			}).Entity;

			_context.SaveChanges();
			
			return _client;
		}

		public Client GetClient(string login)
		{
			var _client =  _context.Clients.SingleOrDefault(c => c.Login == login);
			if (_client == null)
				throw new ClientNotFoundException($"Client with login={login} was not found");
			else
				return _client;
		}

		public bool CheckLogin(string login, string password)
        {
            var _client = _context.Clients.Where(i => i.Login == login).SingleOrDefault();
            
            if (_client != null) {
                return Password.Validate(password, _client.Salt, _client.Password);
             }
             return false;
        }

        public void SaveRefreshToken(string login, string _refreshToken)
        {
			var _client = _context.Clients.SingleOrDefault(c => c.Login == login);
			_client.refreshToken = _refreshToken;
			_context.SaveChanges();
        }

        public string FindRefreshToken(string refToken)
        {
			var _client= _context.Clients.Where(c => c.refreshToken == refToken).SingleOrDefault();
			if (_client != null)
				return _client.refreshToken;
			else
				return null;
        }

		public Token CreateToken(string token){
			
			var _client = _context.Clients.SingleOrDefault(c => c.refreshToken == token);
			var claims = new[]
			{
				new Claim(ClaimTypes.NameIdentifier, _client.Login),
				new Claim(ClaimTypes.Name, _client.Login)
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var newToken = new JwtSecurityToken
			(
				issuer: "AdvertApi",
				audience: "Clients",
				claims: claims,
				expires: DateTime.Now.AddMinutes(10),
				signingCredentials: creds
			);

			var refreshToken = Guid.NewGuid();
			var accessToken = new JwtSecurityTokenHandler().WriteToken(newToken);

			SaveRefreshToken(_client.Login, refreshToken.ToString());

			return new Token 
			{
				AccessToken = accessToken,
				RefreshToken = refreshToken
			}
			;
		}

		public Token CreateFirstToken(string login){
			
			var _client = _context.Clients.SingleOrDefault(c => c.Login == login);
			var claims = new[]
			{
				new Claim(ClaimTypes.NameIdentifier, _client.Login),
				new Claim(ClaimTypes.Name, _client.Login)
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var newToken = new JwtSecurityToken
			(
				issuer: "AdvertApi",
				audience: "Clients",
				claims: claims,
				expires: DateTime.Now.AddMinutes(10),
				signingCredentials: creds
			);

			var refreshToken = Guid.NewGuid();
			var accessToken = new JwtSecurityTokenHandler().WriteToken(newToken);

			SaveRefreshToken(_client.Login, refreshToken.ToString());

			return new Token 
			{
				AccessToken = accessToken,
				RefreshToken = refreshToken
			}
			;
		}

		public ICollection<CampaignResponse> GetCampaigns(string login)
		{
			var _response = from c in _context.Clients
					join cc in _context.Campaigns on c.IdClient equals cc.IdClient
					join b in _context.Banners on cc.IdCampaign equals b.IdCampaign
					orderby cc.StartDate descending
					select new CampaignResponse 
					  {
						FirstName = c.FirstName,
						LastName = c.LastName,
						CampaignStartDate = cc.StartDate,
						BannerName = b.Name
					  };
			Console.WriteLine(_response);
			return _response.ToList();
		}

		public ICollection<CampaignResponse> AddCampaign(CampaignAddingRequest request)
		{
			var _response = from c in _context.Clients
					join cc in _context.Campaigns on c.IdClient equals cc.IdClient
					join b in _context.Banners on cc.IdCampaign equals b.IdCampaign
					orderby cc.StartDate descending
					select new CampaignResponse 
					  {
						FirstName = c.FirstName,
						LastName = c.LastName,
						CampaignStartDate = cc.StartDate,
						BannerName = b.Name
					  };
			Console.WriteLine(_response);
			return _response.ToList();
		}

		CampaignResponse IAdvertDbService.AddCampaign(CampaignAddingRequest request)
		{
			var _fromBuilding = _context.Buildings.FirstOrDefault(b => b.IdBuilding == request.FromIdBuilding);
			if (_fromBuilding == null)
				throw new BuidlingDoesNotExistException($"Building with id = {request.FromIdBuilding} does not exists");
			var _toBuilding = _context.Buildings.FirstOrDefault(b => b.IdBuilding == request.ToIdBuilding);
			if (_toBuilding == null)
				throw new BuidlingDoesNotExistException($"Building with id = {request.FromIdBuilding} does not exists");
			
			if (_fromBuilding.Street != _toBuilding.Street)
				throw new StreetDoesNotMatchException($"Streets do not match {_fromBuilding.Street},{_toBuilding.Street}");
			
			return null;
		}


		public double Test(){
			var nFrom = 1;
			var nTo = 4;

			var area1 = _context.Buildings.Where(b => b.IdBuilding == nFrom ).Select(b => b.Height).Max()*nFrom;
			var area2 = _context.Buildings.Where(b => b.IdBuilding == nTo).Select(b => b.Height).Max()*nTo;

			double area = area1 + area2;

			for (int i = nFrom; i < nTo; i++){
				area1 = _context.Buildings.Where(b => b.IdBuilding >= nFrom && b.IdBuilding <= i).Select(b => b.Height).Max()*(i - nFrom + 1);
				area2 = _context.Buildings.Where(b => b.IdBuilding >= i + 1 && b.IdBuilding <= nTo).Select(b => b.Height).Max()*(nTo - i + 1);
				var areaNew = area1 + area2;
				if (areaNew < area) {
					area = areaNew;
				}	
			}

			return area;
		}
	}
}
