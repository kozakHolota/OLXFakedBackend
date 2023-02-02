using System;
namespace OLXFakedBackend.Models.Api
{
    public class ItemApi
    {
        public int itemId { get; set; }
        public string name { get; set; }
        public string subject { get; set; }
        public string category { get; set; }
        public string description { get; set; }
        public List<string>? images { get; set; }
        public bool autoContinue { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string? city { get; set; }
    }
}

