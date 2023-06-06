namespace CoreLibrary.Models.Api.Product.Responses
{
	public class SuperCategory : CategoryApi
	{
		public List<CategoryApi> SubCategories { get; set; }
	}
}

