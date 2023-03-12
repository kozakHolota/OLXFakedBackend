using System;
namespace OLXFakedBackend.Models.Api.Product.Requests
{
	public class CitiesRequest: PaginatedRequest
    {
		public string? namePart { get; set; }

    }
}

