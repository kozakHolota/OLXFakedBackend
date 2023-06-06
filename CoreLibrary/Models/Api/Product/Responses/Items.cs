namespace CoreLibrary.Models.Api.Product.Responses
{
	public class Items
	{
        public int page { get; set; }
        public int pages { get; set; }
        public List<ItemApi> items { get; set; }
    }
}

