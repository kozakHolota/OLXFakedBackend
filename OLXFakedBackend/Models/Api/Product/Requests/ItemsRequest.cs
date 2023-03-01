using System;
namespace OLXFakedBackend.Models.Api.Product.Requests
{
	public class ItemsRequest: PaginatedRequest
	{
        public string category { get; set; }
        public string cityPart { get; set; }
        public string itemKeyword { get; set; }

    }
}

