using System;
using System.ComponentModel.DataAnnotations;

namespace OLXFakedBackend.Models
{
	public class Category
	{
		[Key]
		public long Id { get; set; }
		public string Name { get; set; }
		public long? ParentCategoryId { get; set; }
		public Image? CategoryIcon { get; set; }
    }
}

