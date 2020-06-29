using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertApi.DTO.Requests;
using Cw11_WebApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AdvertApi.Controllers
{
	[Route("api/campaigns")]
	[ApiController]
	public class CampaignsController : ControllerBase
    {

		private readonly IAdvertDbService _dbService;
		
		public CampaignsController(IAdvertDbService dbService)
		{
			_dbService = dbService;
		}

		[Authorize]
		[HttpGet("{id}")]
		public IActionResult GetCampaigns(string id)
		{
			var _campaigns = _dbService.GetCampaigns(id);
			if (_campaigns != null)
				return Ok(_campaigns);
			else
				return NotFound("Login not found");
		}

		[HttpPost]
		public IActionResult AddCampaign(CampaignAddingRequest request)
		{	
			var campaign = _dbService.AddCampaign(request);
			return new CreatedAtRouteResult("api/campaigns", campaign);
			
		}
	}
}
