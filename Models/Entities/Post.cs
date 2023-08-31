using System;
namespace perpark_api.Models.Entities
{
	public class Post
	{
		public int PostId { get; set; }
		public int UserId { get; set; }
		public string? PostImageUrl { get; set; }
		public string? PostText { get; set; }
		public DateTime datetime { get; set; }
	}
}

