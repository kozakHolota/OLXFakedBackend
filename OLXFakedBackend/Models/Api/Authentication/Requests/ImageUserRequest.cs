using System;
namespace OLXFakedBackend.Models.Api.Authentication.Requests
{
	public class ImageUserRequest
	{
		public string fileName { get; set; }
		public string base64Content { get; set; }
	}
}

