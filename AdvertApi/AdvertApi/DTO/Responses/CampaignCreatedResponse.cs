using AdvertApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace AdvertApi.DTO.Responses
{
	public class CampaignCreatedResponse
	{
		public Campaign Campaign { get; set; }
		//public int IdClient { get; set; }
		//public DateTime StartDate { get; set; }
		//public DateTime EndDate { get; set; }

		//public double PricePerSquareMeter { get; set; }

		//public int FromIdBuilding { get; set; }

		//public int ToIdBuilding { get; set; }

		public Banner  Banner1 { get; set; }

		public Banner  Banner2 { get; set; }
	}
}
