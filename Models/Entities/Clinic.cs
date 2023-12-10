using System;
using System.ComponentModel.DataAnnotations;

namespace perpark_api.Models.Entities
{
	public class Clinic
	{
		[Key]
		public int ClinicId { get; set; }

		public string Name { get; set; }

		public int CityId { get; set; }

		public int DistrictId { get; set; }

		public string? Address { get; set; }

		public string? Phone { get; set; }

		public int? VeterinaryId { get; set; }

		public bool? IsApproved { get; set; }

		public string? Lat { get; set; }

		public string? Lng { get; set; }

		public string? Level { get; set; }

		public DateTime? LastDate { get; set; }

	}
}

