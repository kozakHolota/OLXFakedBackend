using System;
using System.ComponentModel.DataAnnotations;

namespace OLXFakedBackend.Models
{
	public class Requisites
	{
		[Key]
		public long Id { get; set; }
		public string LowName { get; set; }
		public string LowAddress { get; set; }
		public int ZipCode { get; set; }
        public City? City { get; set; }
		public string SingleRegId { get; set; }
		public bool IsTaxesPayer { get; set; }
		public int TaxationId { get; set; }
		public string ContactPersonName { get; set; }
    }
}

