using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ChoETL;

namespace OLXFakedBackend.Models
{
	public class Category
	{
		[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }
        public string Name { get; set; }
		public int? ParentCategoryId { get; set; }
		public string? CategoryIcon { get; set; }
    }
}

