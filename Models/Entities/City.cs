using System;
using System.ComponentModel.DataAnnotations;

namespace perpark_api.Models.Entities
{
	public class City
	{
		
		[Key]
		public int CityId{ get; set; }
		public string Name { get; set; }
		
    }
}

