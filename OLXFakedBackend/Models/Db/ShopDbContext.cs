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

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<City>().HasData(
                new City { CityId = 1, Name = "Київ, Київська область" },
                new City { CityId = 2, Name = "Львів, Львівська область" },
                new City { CityId = 3, Name = "Житомир, Житомирська область" },
                new City { CityId = 4, Name = "Чернігів, Чернігівська" },
                new City { CityId = 5, Name = "Ужгород, Закарпатська область" },
                new City { CityId = 6, Name = "Тернопіль, Тернопільська область" },
                new City { CityId = 7, Name = "Івано-Франківськ, Іванофранківська область" },
                new City { CityId = 8, Name = "Хмельницьк, Хмельницька область" },
                new City { CityId = 9, Name = "Миколаїв, Миколаївська область" },
                new City { CityId = 10, Name = "Одеса, Одеська область" },
                new City { CityId = 11, Name = "Харків, Харківська область" },
                new City { CityId = 12, Name = "Суми, Сумська область" },
                new City { CityId = 13, Name = "Луцьк, Волинська область" },
                new City { CityId = 14, Name = "Рівне, Рівненська область" },
                new City { CityId = 15, Name = "Чернівці, Чернівецька область" },
                new City { CityId = 16, Name = "Дніпро, Дніпропетровська область" },
                new City { CityId = 17, Name = "Запоріжжя, Запорізька область" },
                new City { CityId = 18, Name = "Полтава, Полтавська область" }
                );
        }
    }
}

