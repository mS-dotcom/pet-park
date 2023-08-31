using System;
using System.ComponentModel.DataAnnotations;

namespace perpark_api.Models.Entities
{
	public class UserFollowers
	{
		[Key]
		public int Id { get; set; }
		public int FollowingId { get; set; }
		public int FollowersId { get; set; }
	}
}

