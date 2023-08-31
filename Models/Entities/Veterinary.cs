using System;
using System.ComponentModel.DataAnnotations;

namespace perpark_api.Models.Entities
{
	public class Veterinary
	{
        [Key]
        public int VeteniaryId { get; set; }
        public int userId { get; set; }
        public int CityId { get; set; }
        

        public int DistrictId { get; set; }
        public string? Lat { get; set; }
        public string? Lng { get; set; }

    }
}

