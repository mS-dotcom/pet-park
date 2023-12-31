﻿using System;
using perpark_api.Models;
using perpark_api.Models.Entities;

namespace perpark_api.DevelopmentClasses
{
	public class AnimalDetailVM
	{
		public Animal Animal { get; set; }
		public List<Vaccine> Vaccines { get; set; }
        public List<Sickness> Sickness { get; set; }
		public User UserAccount { get; set; }
		public Veterinary VeterinaryAccount { get; set; }
    }
}

