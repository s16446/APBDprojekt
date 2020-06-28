﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdvertApi.Models
{
	public class Client
	{
		public int IdClient { get; set; }

		public string FirstName { get; set; }
		
		public string LastName { get; set; }


		public string Email { get; set; }


		public string Phone { get; set; }

		public string Login { get; set; }

		public string Password { get; set; }

		public string Salt { get; set; }

		public string refreshToken {get; set;}

		public ICollection<Campaign> Campaigns { get; set; }
	}
}