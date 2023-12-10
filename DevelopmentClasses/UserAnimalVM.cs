using System;
using perpark_api.Models;
using perpark_api.Models.Entities;

namespace perpark_api.DevelopmentClasses
{
	public class UserAnimalVM
	{
		public User user { get; set; }

		public Animal Animal { get; set; }

		public List<Vaccine> Vaccines { get; set; }

		public List<Sickness>Sicknesses { get; set; }
	}
}

