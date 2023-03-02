using System;
using System.ComponentModel.DataAnnotations;

namespace OLXFakedBackend.Models.Api
{
	public class UserRegistrationRequest
	{
        [Required]
        public string userId { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string contactPersonName { get; set; }
        public string? email { get; set; }
        public string? phoneNumber { get; set; }
        public string? contactCity { get; set; }
        public string? imagePath { get; set; }
        public string? lowName { get; set; }
        public string? lowAddress { get; set; }
        public int? zipCode { get; set; }
        public string? requisitesCity { get; set; }
        public string? singleRegId { get; set; }
        public bool? isTaxesPayer { get; set; }
        public int? taxationId { get; set; }
        public string? requisitesContactPersonName { get; set; }
    }
}

