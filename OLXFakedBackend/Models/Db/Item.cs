using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OLXFakedBackend.Models
{
	public class Item
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemId { get; set; }
		public string Name { get; set; }
		public Category Category { get; set; }
		public string Description { get; set; }
		public Image? Image { get; set; }
		public bool AutoContinue { get; set; }
		public ContactData ContactData { get; set; }
    }
}

