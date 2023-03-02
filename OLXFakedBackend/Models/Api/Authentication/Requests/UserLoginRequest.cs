using System;
using System.ComponentModel.DataAnnotations;

namespace OLXFakedBackend.Models.Api.Authentication.Requests
{
	public class UserLoginRequest
	{
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

