using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OLXFakedBackend.Models.Db
{
	public class ItemImage
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemImageId { get; set; }
        public int ItemId { get; set; }
        public Image Image { get; set; }

        [ForeignKey(nameof(ItemId))]
        public Item Item { get; set; }
    }
}

