using System;
namespace OLXFakedBackend.Models.Api
{
	public class Items
	{
        public int page { get; set; }
        public int pages { get; set; }
        public List<ItemApi> items { get; set; }
    }
}

