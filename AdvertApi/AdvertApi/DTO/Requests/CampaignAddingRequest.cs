using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertApi.DTO.Requests
{
	public class CampaignAddingRequest
	{
		/*
			 {
			"IdClient": 1,
			"StartDate": "2020-1-1",
			"EndDate": "2020-3-1",
			"PricePerSquareMeter": 35,
			"FromIdBuilding": 1,
			"ToIdBuilding": 4
			}
		 */

		public int IdClient { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate{ get; set; }
		public double PricePerSquareMeter { get; set; }
		public int ToIdBuilding { get; set; }

		public int FromIdBuilding { get; set; }
	}
}
