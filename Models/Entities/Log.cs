using System;
using System.ComponentModel.DataAnnotations;

namespace perpark_api.Models.Entities
{
	public class Log
	{
		[Key]
		public int LogId { get; set; }
		public string? LogDescription { get; set; }
		public int? UserId { get; set; }
		public string LogType { get; set; }
		public DateTime DateTime { get; set; }
	}
}

