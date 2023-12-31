﻿using System;
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
        public string Email { get; set; }
        public DateTime LastDate { get; set; }
        public string? FileLocation { get; set; }
        public bool? IsPrimary { get; set; }
        public bool? HasPayment { get; set; }
        public bool? HasFileSend { get; set; }
        public bool? HasClinicPinned { get; set; }
        public int? ClinicId { get; set; }


    }
}