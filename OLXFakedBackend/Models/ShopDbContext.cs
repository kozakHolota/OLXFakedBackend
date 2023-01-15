using System;
using Microsoft.EntityFrameworkCore;

namespace OLXFakedBackend.Models
{
	public class ShopDbContext : DbContext
    {
		public DbSet<City> City { get; set; }
        public DbSet<ContactPerson> ContactPerson { get; set; }
        public DbSet<Requisites> Requisites { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<ContactData> ContactData { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<UserItem> UserItem { get; set; }

        public ShopDbContext(DbContextOptions options) : base(options) { }
    }
}

