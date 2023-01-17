using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OLXFakedBackend.Models
{
	public class Category
	{
		[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }
		public string Name { get; set; }
		public long? ParentCategoryId { get; set; }
		public Image? CategoryIcon { get; set; }
    }
}

