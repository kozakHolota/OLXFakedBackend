using System;
using System.ComponentModel.DataAnnotations;

namespace OLXFakedBackend.Models
{
	public class City
	{
        [Key]
        public long Id { get; set; }
		public string Name { get; set; }
	}
}

