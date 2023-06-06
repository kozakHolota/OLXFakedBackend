using CoreLibrary.Models.Api.Utils;

namespace CoreLibrary.Models.Api.Product.Responses
{
    public class ItemApi
    {
        public int itemId { get; set; }
        public string name { get; set; }
        public string subject { get; set; }
        public string category { get; set; }
        public string description { get; set; }
        public double price { get; set; }
        public List<ImageApi>? images { get; set; }
        public bool autoContinue { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string? city { get; set; }
        public string? district { get; set; }
    }
}

