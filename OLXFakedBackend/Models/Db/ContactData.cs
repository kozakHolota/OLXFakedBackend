using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OLXFakedBackend.Models
{
	public class ContactData
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContactDataId { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public City? City { get; set; }
    }
}

