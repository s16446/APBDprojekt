using AdvertApi.DTO.Requests;
using Cw11_WebApplication.DAL;
using Cw11_WebApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

		//[Authorize]
		[HttpPost]
		public IActionResult AddCampaign(CampaignAddingRequest request)
		{	
			try {
				var _campaign = _dbService.AddCampaign(request);
				return new CreatedAtRouteResult("api/campaigns", _campaign);
			} 
			catch (StreetDoesNotMatchException exc)
			{
				return BadRequest(exc.Message); // 400
			}
			
		}
	}
}
