using System;
using System.ComponentModel.DataAnnotations;
using OLXFakedBackend.Models.Api.Utils;

namespace OLXFakedBackend.Models.Api.Product.Requests
{
	public class ItemAddRequest
	{
        [Required]
        public string name { get; set; }
        [Required]
        public string subject { get; set; }
        [Required]
        public string category { get; set; }
        [Required]
        public double price { get; set; }
        [Required]
        public string description { get; set; }
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

