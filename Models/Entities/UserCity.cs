using System;
using System.ComponentModel.DataAnnotations;

namespace perpark_api.Models.Entities
{
	public class UserCity
	{
		[Key]
		public int CityId { get; set; }
		public string Name { get; set; }
	}
}

