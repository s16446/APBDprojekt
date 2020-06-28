using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertApi.Models
{
	public class Banner
	{
		public int IdAdvertisement { get; set; }

		public string Name { get; set; }

		public double Price { get; set; }

		public double Area { get; set; }

		public int IdCampaign { get; set; }

		public Campaign Campaign { get; set; }

	}
}
