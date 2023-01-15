using System;
using System.ComponentModel.DataAnnotations;

namespace OLXFakedBackend.Models
{
	public class ContactData
	{
		[Key]
		public long Id { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public City? City { get; set; }
    }
}

