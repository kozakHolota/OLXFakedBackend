using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OLXFakedBackend.Models
{
	public class ContactPerson
	{
        [Key]
        public long Id { get; set; }
		public string Name { get; set; }
		public City? City { get; set; }
	}
}

