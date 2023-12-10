using System;
using perpark_api.Models;
using perpark_api.Models.Entities;

namespace perpark_api.DevelopmentClasses
{
	public class MessageVM
	{
		public List<User> Users { get; set; }
		public List<ChatMessages>? Messages { get; set; }
	}
}

