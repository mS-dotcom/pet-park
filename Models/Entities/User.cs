using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using perpark_api.Models.Entities;

namespace perpark_api.Models
{ 
	public class User
	{
		[Key]
		public int UserId { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public string? ProfilePictureUrl { get; set; }
		public string? Name { get; set; }
		public string? Surname{ get; set; }
		public string? Desc { get; set; }
		public int CityId { get; set; }
		public int DistrictId { get; set; }
        public string? Address { get; set; }
		public string? Lat { get; set; }
        public string? Lng { get; set; }
        public int UserTypeId { get; set; }
		public string? ClinicName { get; set; }
        public string? ClinicPhone { get; set; }
		public string? Token { get; set; }
	}
}