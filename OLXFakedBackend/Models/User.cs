using System;
using System.ComponentModel.DataAnnotations;

namespace OLXFakedBackend.Models
{
	public class User
	{
		[Key]
		public long Id { get; set; }
		public string UserName { get; set; }
		public byte Password { get; set; }
		public string Email { get; set; }
		public ContactPerson? ContactPerson { get; set; }
		public Requisites? Requisites { get; set; }
    }
}

