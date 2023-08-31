using System;
namespace perpark_api.DevelopmentClasses
{
	public class AddAdvertPhotoVM
	{
		public IFormFile? file { get; set; }

		public int? AdvertId { get; set; }
	}
}

