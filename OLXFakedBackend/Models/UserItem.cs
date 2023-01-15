using System;
using System.ComponentModel.DataAnnotations;

namespace OLXFakedBackend.Models
{
	public class UserItem
	{
		[Key]
		public long Id { get; set; }
		public User User { get; set; }
		public Item Item { get; set; }
	}
}

