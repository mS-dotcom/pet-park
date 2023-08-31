using System;
using System.ComponentModel.DataAnnotations;

namespace perpark_api.Models.Entities
{
	public class Animal
	{
		public Animal()
		{
		}
		[Key]
		public int AnimalId { get; set; }
		public string Name { get; set; }
		public int AnimalTypeId { get; set; }
		public string? AnimalTypeText { get; set; }
		public int? AnimalAge { get; set; }
		public bool? Gender { get; set; }
		public string? QRCode { get; set; }
        public string? QRImage { get; set; }
        public int UserId { get; set; }
        public int? VeterinaryId { get; set; }

    }
}