using System;
using System.ComponentModel.DataAnnotations;

namespace OLXFakedBackend.Models.Api
{
	public class TokenRequest
	{
        [Required]
        public string Token { get; set; }
        [Required]
        public string RefreshToken { get; set; }
    }
}

