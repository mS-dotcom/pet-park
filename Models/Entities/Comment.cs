using System;
using System.ComponentModel.DataAnnotations;

namespace perpark_api.Models.Entities
{
	public class Comment
	{
		[Key]
		public int CommentId { get; set; }
		public int? UserId { get; set; }
		public int? PostId { get; set; }
		public string? Content { get; set; }
		public DateTime datetime { get; set; }
	}
}

