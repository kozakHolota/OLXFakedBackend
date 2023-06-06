using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace CoreLibrary.Models.Db
{
	public class UserItem
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserItemId { get; set; }
        public string UserId { get; set; }
        public int ItemId { get; set; }

        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; }
        [ForeignKey(nameof(ItemId))]
        public Item Item { get; set; }
    }
}

