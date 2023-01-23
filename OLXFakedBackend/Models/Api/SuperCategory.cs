using System;
namespace OLXFakedBackend.Models.Api
{
	public class SuperCategory : CategoryApi
	{
		public List<CategoryApi> SubCategories { get; set; }
	}
}

