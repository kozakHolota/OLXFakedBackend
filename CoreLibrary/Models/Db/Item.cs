using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreLibrary.Models.Db
{
	public class Item
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemId { get; set; }
		public string Name { get; set; }
		public string Subject { get; set; }
		public int CategoryId { get; set; }
		public string Description { get; set; }
		public bool AutoContinue { get; set; }
		public ContactData ContactData { get; set; }
		public double Price { get; set; }

        [ForeignKey(nameof(CategoryId))]
		public Category Category { get; set; }
    }
}

