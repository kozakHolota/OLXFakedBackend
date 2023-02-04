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
        public Item Item { get; set; }
        public Image Image { get; set; }
    }
}

