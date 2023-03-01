using System;
using System.ComponentModel.DataAnnotations;

namespace OLXFakedBackend.Models.Api.Product.Responses
{
	public class UserPreferences
	{
        [Required]
        public string UserId { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        [Required]
        public string ContactPersonName { get; set; }
        public string? ContactCity { get; set; }
        public string? ImagePath { get; set; }
        public string? LowName { get; set; }
        public string? LowAddress { get; set; }
        public int? ZipCode { get; set; }
        public string? RequisitesCity { get; set; }
        public string? SingleRegId { get; set; }
        public bool? IsTaxesPayer { get; set; }
        public int? TaxationId { get; set; }
        public string? RequisitesContactPersonName { get; set; }
    }
}

