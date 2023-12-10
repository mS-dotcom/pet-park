using System;
using perpark_api.Models.Entities;

namespace perpark_api.DevelopmentClasses
{
	public class PostDetailVM
	{
		public int PostId { get; set; }
		public int? UserId { get; set; }
		public string? UserName { get; set; }
		public string? OwnerPPUrl { get; set; }
		public string? PostImageUrl { get; set; }
		public string? PostText { get; set; }
		public int PostLikeCount { get; set; }
		public bool? IsLikeOwner { get; set; }
		public DateTime datetime { get; set; }
		public List<CommentVM> Comments { get; set; }
		public int CommentCount { get; set; }
	}
}