using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OLXFakedBackend.Models
{
	public class ContactPerson
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContactPersonId { get; set; }
		public string Name { get; set; }
		public City? City { get; set; }
	}
}

