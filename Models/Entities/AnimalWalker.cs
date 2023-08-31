using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace perpark_api.Models.Entities
{
	public class AnimalWalker
	{
        [Key]
        public int AnimalWalkerId { get; set; }

        public int UserId { get; set; }
        public int CityId { get; set; }
        
        
        public int DistrictId { get; set; }
        

        public string? Lat { get; set; }
        public string? Lng { get; set; }
    }
}

