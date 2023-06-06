using System.ComponentModel.DataAnnotations;
using CoreLibrary.Models.Api.Utils;

namespace CoreLibrary.Models.Api.Product.Requests
{
	public class ItemAddRequestDb
	{
        [Required]
        public string userId { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string subject { get; set; }
        [Required]
        public string category { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public double price { get; set; }
        [Required]
        public bool autoContinue { get; set; }
        [Required]
        public string contactEmail { get; set; }
        [Required]
        public string contactPhone { get; set; }
        public string? contactCity { get; set; }
        public List<ImageApi>? images { get; set; }
    }
}

