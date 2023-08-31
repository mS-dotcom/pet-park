using System;
using perpark_api.Models;
using perpark_api.Models.Entities;

namespace perpark_api.DevelopmentClasses
{
	public class AnimalDetailVM
	{
		public Animal Animal { get; set; }
		public List<Vaccine> Vaccines { get; set; }
        public List<Sickness> Sickness { get; set; }
		public User user { get; set; }
		public Veterinary veterinary { get; set; }
    }
}

