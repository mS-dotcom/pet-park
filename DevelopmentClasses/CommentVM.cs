using System;
namespace perpark_api.DevelopmentClasses
{
	public class CommentVM
	{
		public string? UserPP { get; set; }
		public string? UserName { get; set; }
        public int? UserId { get; set; }
        public int CommentId { get; set; }
        public int? PostId { get; set; }
        public string? Content { get; set; }
        public DateTime datetime { get; set; }
    }
}

