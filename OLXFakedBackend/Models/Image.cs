using System;
using System.ComponentModel.DataAnnotations;

namespace OLXFakedBackend.Models
{
	public class Image
	{
		[Key]
		public long Id { get; set; }
		public string Path { get; set; }

    }
}

