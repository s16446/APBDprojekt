using AdvertApi.Controllers;
using AdvertApi.DTO.Requests;
using AdvertApi.Models;
using System.Collections;
using System.Collections.Generic;

namespace Cw11_WebApplication.Services
{
	public interface IAdvertDbService
    {

        Client AddClient(ClientRegistrationRequest request);

		Client GetClient(string login);

		bool CheckLogin(string login, string password);

		void SaveRefreshToken(string login, string v);

		string FindRefreshToken(string token);
		
		Token CreateToken(string login);

		ICollection<CampaignResponse> GetCampaigns(string login);
		CampaignResponse AddCampaign(CampaignAddingRequest request);
		double Test();
		Token CreateFirstToken(string refreshToken);
	}
}
