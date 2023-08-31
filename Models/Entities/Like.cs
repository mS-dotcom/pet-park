using System;
using System.ComponentModel.DataAnnotations;

namespace perpark_api.Models.Entities
{
	public class Like
	{
		[Key]
		public int LikeId { get; set; }
		public int UserId { get; set; }
		public int PostId { get; set; }
	}
}

