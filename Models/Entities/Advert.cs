using System;
using System.ComponentModel.DataAnnotations;

namespace perpark_api.Models.Entities
{
	public class Advert
	{
		[Key]
		public int AdvertId { get; set; }

		public int UserId { get; set; }

		public string AdvertHeader { get; set; }

		public string AdvertDescription { get; set; }

		public int CityId { get; set; }

		public int DistrictId { get; set; }

		public string? AdvertImageLocation { get; set; }

		public bool? AnimalInfoShare { get; set; }

		public int? AnimalId { get; set; }

	}
}

