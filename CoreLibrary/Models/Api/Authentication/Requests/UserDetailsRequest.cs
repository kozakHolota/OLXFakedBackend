using System.ComponentModel.DataAnnotations;

namespace CoreLibrary.Models.Api.Authentication.Requests
{
	public class UserDetailsRequest
	{
        [Required]
        public string Token { get; set; }
    }
}

