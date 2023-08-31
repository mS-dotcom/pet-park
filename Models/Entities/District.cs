﻿using System;
using System.ComponentModel.DataAnnotations;

namespace perpark_api.Models.Entities
{
	public class District
	{
		[Key]
		public int? DistrictId { get; set; }
		public int? CityId { get; set; }
		
		public string Name { get; set; }
        

    }
}

