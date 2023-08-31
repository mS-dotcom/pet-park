using System;
using System.ComponentModel.DataAnnotations;

namespace perpark_api.Models.Entities
{
	public class PetHotel
	{
        [Key]
        public int PetHotelId { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        public int CityId { get; set; }
        

        public int DistrictId { get; set; }

        public string? Address { get; set; }
    }
}

