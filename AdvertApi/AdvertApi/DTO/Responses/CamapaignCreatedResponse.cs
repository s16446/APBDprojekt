using AdvertApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace AdvertApi.DTO.Responses
{
	public class CamapaignCreatedResponse
	{
		public Campaign  Campaign { get; set; }

		public ICollection<Banner>  Banners { get; set; }
	}
}
