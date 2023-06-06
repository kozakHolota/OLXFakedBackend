using System.ComponentModel.DataAnnotations;

namespace CoreLibrary.Models.Api.Authentication.Requests
{
	public class TokenRequest
	{
        [Required]
        public string Token { get; set; }
        [Required]
        public string RefreshToken { get; set; }
    }
}

