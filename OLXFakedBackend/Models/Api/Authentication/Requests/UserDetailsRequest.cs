using System;
using System.ComponentModel.DataAnnotations;

namespace OLXFakedBackend.Models.Api.Authentication.Requests
{
	public class UserDetailsRequest
	{
        [Required]
        public string Token { get; set; }
    }
}

