using System;
namespace OLXFakedBackend.Models.Api.Authentication.Requests
{
	public class ImageItemRequest
	{
        public string fileName { get; set; }
        public string base64Content { get; set; }
        public bool isFavorite { get; set; }
    }
}

