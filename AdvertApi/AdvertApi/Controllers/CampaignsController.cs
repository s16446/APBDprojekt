using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertApi.DTO.Requests;
using Cw11_WebApplication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AdvertApi.Controllers
{
	[Route("api/campaigns")]
	[ApiController]
	public class CampaignsController : ControllerBase
    {
        private IConfiguration Configuration { get; set; }

		private readonly IAdvertDbService _dbService;
		
		public CampaignsController(IAdvertDbService dbService)
		{
			_dbService = dbService;
		}

		[HttpGet]
		public IActionResult GetTest()
		{	
			return Ok(_dbService.Test());
		}

		[HttpPost]
		public IActionResult AddCampaign(CampaignAddingRequest request)
		{	
			var campaign = _dbService.AddCampaign(request);
			return new CreatedAtRouteResult("api/campaigns", campaign);
			
		}
	}
}
